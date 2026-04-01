using UnityEngine;

public class GoblinChase1 : EnemyState
{
    public GoblinChase1(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _enemy.MovementCompo.SetMovement((_enemy.targetTrm.position - _enemy.transform.position).normalized);
        if (Vector2.Distance(_enemy.targetTrm.position, _enemy.transform.position) < 5)
        {
            _stateMachine.ChangeState(GoblinEnum.GoblinIdle);
            _enemy.MovementCompo.SetMovement(Vector2.zero);
            return;
        }
    }

}
