using UnityEngine;

public class DemonIdle : EnemyState
{
    public DemonIdle(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();

        Collider2D player = _enemy.detectPlayer();
        if (player != null && Player.IsCombat)
        {
            _enemy.targetTrm = player.transform;
            _stateMachine.ChangeState(DemonEnum.DemonChase);
        }
    }
}
