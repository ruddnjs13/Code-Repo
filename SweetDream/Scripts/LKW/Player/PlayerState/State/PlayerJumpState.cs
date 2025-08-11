using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.RbCompo.velocity = new Vector2(_player.RbCompo.velocity.x, 0f);
        _player.RbCompo.AddForce(Vector2.up * _player._jumpPower, ForceMode2D.Impulse);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (_player.RbCompo.velocity.y < -0)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Fall);
        }
    }

}
