using UnityEngine;

public class GoblinIdle : EnemyState
{
    public GoblinIdle(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }


    public override void UpdateState()
    {
        base.UpdateState();
        Collider2D player = _enemy.detectPlayer();
        if (player != null && Player.IsCombat)
        {
            _enemy.isPlayerFind = true;
            _enemy.targetTrm = player.transform;
            if (Vector2.Distance(_enemy.transform.position, player.transform.position) > 6)
            {
                _stateMachine.ChangeState(GoblinEnum.GoblinChase1);
                return;
            }
            if (Vector2.Distance(_enemy.transform.position, player.transform.position) < 2)
            {
                _stateMachine.ChangeState(GoblinEnum.GoblinChase2);
                return;
            }
        }
    }

}
