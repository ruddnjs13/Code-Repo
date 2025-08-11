using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    
    
    public PlayerGroundState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        _player.playerInput.JumpEvent += HandleJumpEvent;
        base.Enter();
        if (_player.bufferCount > 0)
        {
            _player.StateMachine.ChangeState(PlayerStateEnum.Jump);
            _player.bufferCount = 0f;
        }
        _player.canFlip = true;
    }

    public override void Exit()
    {
        _player.playerInput.JumpEvent -= HandleJumpEvent;
        base.Exit();
    }

    private void HandleJumpEvent()
    {
        if (_player.IsGround.Value)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Jump);
        }
    }

    public override void StateUpdate()
    {
        if (!_player.IsGround.Value)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Fall);
        }
    }

    
}
