using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBossAttack3 : EnemyState
{
    public MagicBossAttack3(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Exit()
    {
        base.Exit();
        _enemy.lastAttackTime = Time.time;
    }


    public override void UpdateState()
    {
        base.UpdateState();
        if (_endTriggerCalled)
        {
            _stateMachine.ChangeState(MagicBossEnum.BossMove);
        }
    }
}
