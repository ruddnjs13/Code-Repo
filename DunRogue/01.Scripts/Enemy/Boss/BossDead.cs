using UnityEngine;

public class BossDead : EnemyState
{
    public BossDead(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.canStateChangeable = false;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _enemy.MovementCompo.SetMovement(Vector3.zero);
        if (_endTriggerCalled)
        {
            GameObject.Destroy(_enemy.gameObject);
        }
    }
}
