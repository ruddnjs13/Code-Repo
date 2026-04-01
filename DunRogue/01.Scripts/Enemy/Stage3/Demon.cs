using UnityEngine;

public enum DemonEnum
{
    DemonIdle,
    DemonChase,
    DemonExplosion

}

public class Demon : Enemy, IPoolable
{
    [HideInInspector] public Vector2 movement;

    public EnemyStateMachine stateMachine;

    public string PoolName => gameObject.name;

    public GameObject objectPrefab => gameObject;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();

        stateMachine.AddState(DemonEnum.DemonIdle, new DemonIdle(this, stateMachine, "Idle"));
        stateMachine.AddState(DemonEnum.DemonChase, new DemonChase(this, stateMachine, "Chase"));
        stateMachine.AddState(DemonEnum.DemonExplosion, new DemonExplosion(this, stateMachine, "Explosion"));


        stateMachine.Initialize(DemonEnum.DemonIdle, this);
    }

    private void Update()
    {
        stateMachine.CurrentState.UpdateState();
        if (isAttack)
        {
            Dead();
        }
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
