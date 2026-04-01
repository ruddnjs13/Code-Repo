using DG.Tweening;
using UnityEngine;

public class MiniEnemyAttack : EnemyState
{
    Tween attacktween;

    public MiniEnemyAttack(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.MovementCompo.StopImmediately(false);
        attacktween = _enemy.transform.DOMove(_enemy.targetTrm.position, 0.4f)
            .SetEase(Ease.InOutQuint)
            .SetLoops(2, LoopType.Yoyo);
    }

    public override void Exit()
    {
        _enemy.lastAttackTime = Time.time;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_endTriggerCalled)
        {
            _stateMachine.ChangeState(MiniEnemyEnum.Chase);
        }
    }
}
