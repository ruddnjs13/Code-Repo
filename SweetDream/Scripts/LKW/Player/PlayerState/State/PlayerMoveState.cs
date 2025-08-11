using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine ,string animBoolName) : base(player, stateMachine ,animBoolName)
    {
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        _player.SetMovement(new Vector2(_player.playerInput.Movement.x * _player._moveSpeed,_player.RbCompo.velocity.y));
            
        if (Mathf.Abs(_player.playerInput.Movement.x) < 0.01f)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }
}
