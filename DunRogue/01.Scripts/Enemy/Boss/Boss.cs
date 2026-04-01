using UnityEngine;

public enum BossEnum
{
    BossIdle,
    BossChase,
    BossAttack,
    BossSpell,
    BossSummon,
    BossDead
}

public class Boss : Enemy
{
    [HideInInspector] public Vector2 movement;

    public EnemyStateMachine stateMachine;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        stateMachine.AddState(BossEnum.BossIdle, new BossIdle(this, stateMachine, "Idle"));
        stateMachine.AddState(BossEnum.BossChase, new BossChase(this, stateMachine, "Walk"));
        stateMachine.AddState(BossEnum.BossAttack, new BossAttack(this, stateMachine, "Attack"));
        stateMachine.AddState(BossEnum.BossSpell, new BossSpell(this, stateMachine, "Spell"));
        stateMachine.AddState(BossEnum.BossSummon, new BossSummon(this, stateMachine, "Spell"));
        stateMachine.AddState(BossEnum.BossDead, new BossDead(this, stateMachine, "Dead"));


        stateMachine.Initialize(BossEnum.BossIdle, this);
    }

    private void Update()
    {
        stateMachine.CurrentState.UpdateState();
        if (targetTrm != null)
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
        stateMachine.CurrentState.AnimationEndtrigger();
        stateMachine.CurrentState.Exit();
        stateMachine.ChangeState(BossEnum.BossDead);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + new Vector3(0,-1,0), boxSize);
    }
#endif
}
