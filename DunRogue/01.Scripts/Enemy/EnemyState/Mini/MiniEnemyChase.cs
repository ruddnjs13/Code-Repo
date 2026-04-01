using UnityEngine;

public class MiniEnemyChase : EnemyState
{
    public MiniEnemyChase(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();

        float distance = Vector2.Distance(_enemy.targetTrm.position, _enemy.transform.position);
        _enemy.MovementCompo.SetMovement((_enemy.targetTrm.position - _enemy.transform.position).normalized);

        if (distance < _enemy.attackRadius && _enemy.lastAttackTime + _enemy.attackCooltime < Time.time)
        {
            _stateMachine.ChangeState(MiniEnemyEnum.Attack);
            return;
        }

    }

}
