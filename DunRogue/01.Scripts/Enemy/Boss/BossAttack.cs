using UnityEngine;

public class BossAttack : EnemyState
{
    public BossAttack(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Exit()
    {
        _enemy.lastAttackTime = Time.time;
        base.Exit();
        return;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_endTriggerCalled)
        {
            _enemy.isAttack = false;
            _stateMachine.ChangeState(BossEnum.BossChase);
        }
    }
}
