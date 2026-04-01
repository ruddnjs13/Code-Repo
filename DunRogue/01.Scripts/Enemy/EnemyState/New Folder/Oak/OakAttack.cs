using DG.Tweening;
using UnityEngine;

public class OakAttack : EnemyState
{
    Tween attacktween;

    public OakAttack(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
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
        return;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_endTriggerCalled)
        {
            _enemy.isAttack = false;
            _stateMachine.ChangeState(OakEnum.OakChase);
        }
    }
}
