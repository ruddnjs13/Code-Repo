using UnityEngine;

public enum OakEnum
{
    OakIdle,
    OakChase,
    OakAttack
}


public class Oak : Enemy, IPoolable
{
    [HideInInspector] public Vector2 movement;

    public EnemyStateMachine stateMachine;

    public string PoolName => gameObject.name;

    public GameObject objectPrefab => gameObject;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        stateMachine.AddState(OakEnum.OakIdle, new OakIdle(this, stateMachine, "Idle"));
        stateMachine.AddState(OakEnum.OakChase, new OakChase(this, stateMachine, "Chase"));
        stateMachine.AddState(OakEnum.OakAttack, new OakAttack(this, stateMachine, "Attack"));

        stateMachine.Initialize(OakEnum.OakIdle, this);
    }

    private void Update()
    {
        stateMachine.CurrentState.UpdateState();
    }

    private void FixedUpdate()
    {
        if (targetTrm != null && !isDead)
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
