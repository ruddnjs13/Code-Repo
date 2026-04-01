using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public enum MagicBossEnum
{
    BossIdle,
    BossMove,
    BossAttack1,
    BossAttack2,
    BossAttack3,
    BossDead
}

public class MagicBoss : Enemy
{
    [HideInInspector] public Vector2 movement;

    public EnemyStateMachine stateMachine;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        stateMachine.AddState(MagicBossEnum.BossIdle, new MagicBossIdle(this, stateMachine, "Idle"));
        stateMachine.AddState(MagicBossEnum.BossMove, new MagicBossMove(this, stateMachine, "Move"));
        stateMachine.AddState(MagicBossEnum.BossAttack1, new MagicBossAttack1(this, stateMachine, "Attack1"));
        stateMachine.AddState(MagicBossEnum.BossAttack2, new MagicBossAttack2(this, stateMachine, "Attack2"));
        stateMachine.AddState(MagicBossEnum.BossAttack3, new MagicBossAttack3(this, stateMachine, "Attack3"));
       

        stateMachine.Initialize(MagicBossEnum.BossIdle, this);
    }

    private void Update()
    {
        stateMachine.CurrentState.UpdateState();
        if (targetTrm != null && Player.IsCombat)
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
        stateMachine.ChangeState(MagicBossEnum.BossDead);
    }
}
