using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public enum GoblinEnum
{
    GoblinIdle,
    GoblinChase1,
    GoblinChase2,
    GoblinAttack
}

public class Goblin : Enemy, IPoolable
{
    [HideInInspector] public Vector2 movement;

    public EnemyStateMachine stateMachine;

    private bool _attackCoolTime;
    private float minCool = 2f;
    private float maxCool = 4.5f;

    private Transform _firePos;

    public string PoolName => gameObject.name;

    public GameObject objectPrefab => gameObject;

    protected override void Awake()
    {
        base.Awake();
        _firePos = transform.Find("WeaponHolder").Find("Weapon").Find("FirePos");
        stateMachine = new EnemyStateMachine();

        stateMachine.AddState(GoblinEnum.GoblinIdle, new GoblinIdle(this, stateMachine, "Idle"));
        stateMachine.AddState(GoblinEnum.GoblinChase1, new GoblinChase1(this, stateMachine, "Chase"));
        stateMachine.AddState(GoblinEnum.GoblinChase2, new GoblinChase2(this, stateMachine, "Chase"));


        stateMachine.Initialize(GoblinEnum.GoblinIdle, this);
    }

    private void Update()
    {
        stateMachine.CurrentState.UpdateState();
        if (!_attackCoolTime && isPlayerFind)
        {
            EnemyBullet bullet = PoolManager.Instance.Pop("EnemyBullet") as EnemyBullet;
            bullet.transform.SetPositionAndRotation(_firePos.position, _firePos.rotation);
            _attackCoolTime = true;
            StartCoroutine(AttackCooltime());
        }
    }

    private IEnumerator AttackCooltime()
    {
        yield return new WaitForSeconds(Random.Range(minCool, maxCool));
        _attackCoolTime = false;
    }

    private void FixedUpdate()
    {
        if (targetTrm != null && !isDead && transform.position.x != targetTrm.position.x)
        {
            Flip(targetTrm.position);
        }
    }

    public override void AnimationEndTrigger()
    {
        stateMachine.CurrentState.AnimationEndtrigger();
    }

    public void Dead()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
        PoolManager.Instance.Push(this);
    }

    public void ResetItem()
    {
    }
}
