using UnityEngine;

public class DemonChase : EnemyState
{
    public DemonChase(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();

        float distance = Vector2.Distance(_enemy.targetTrm.position, _enemy.transform.position);

        _enemy.MovementCompo.SetMovement((_enemy.targetTrm.position - _enemy.transform.position).normalized);
        if (distance < _enemy.attackRadius)
        {
            _stateMachine.ChangeState(DemonEnum.DemonExplosion);
            return;
        }

    }

}
