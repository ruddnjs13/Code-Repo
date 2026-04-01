using UnityEngine;


public class OakIdle : EnemyState
{
    public OakIdle(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
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
            _enemy.targetTrm = player.transform;
            _stateMachine.ChangeState(OakEnum.OakChase);
        }
    }

}
