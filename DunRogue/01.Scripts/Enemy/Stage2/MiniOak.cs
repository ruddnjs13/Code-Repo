using UnityEngine;


public class MiniOak : Enemy, IPoolable
{
    [HideInInspector] public Vector2 movement;

    public EnemyStateMachine stateMachine;

    [SerializeField] private int _damage = 10;

    public string PoolName => gameObject.name;

    public GameObject objectPrefab => gameObject;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        stateMachine.AddState(MiniEnemyEnum.Idle, new MiniEnemyIdle(this, stateMachine, "Idle"));
        stateMachine.AddState(MiniEnemyEnum.Chase, new MiniEnemyChase(this, stateMachine, "Chase"));
        stateMachine.AddState(MiniEnemyEnum.Attack, new MiniEnemyAttack(this, stateMachine, "Attack"));

        stateMachine.Initialize(MiniEnemyEnum.Idle, this);
    }

    private void Update()
    {
        stateMachine.CurrentState.UpdateState();
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


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player") && isAttack)
        {
            damageCaster.CastDamage(_damage);
            isAttack = false;
        }
    }



    public void ResetItem()
    {
    }
}

