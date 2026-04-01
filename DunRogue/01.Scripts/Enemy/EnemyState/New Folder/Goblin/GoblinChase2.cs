using UnityEngine;

public class GoblinChase2 : EnemyState
{
    public GoblinChase2(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _enemy.MovementCompo.SetMovement(((_enemy.targetTrm.position - _enemy.transform.position) * -1).normalized);
        if (Mathf.Abs(Vector2.Distance(_enemy.targetTrm.position, _enemy.transform.position)) > 2.5f)
        {
            _stateMachine.ChangeState(GoblinEnum.GoblinIdle);
            _enemy.MovementCompo.SetMovement(Vector2.zero);
            return;
        }
    }
}
