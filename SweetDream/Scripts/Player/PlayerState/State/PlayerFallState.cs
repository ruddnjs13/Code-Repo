using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.playerInput.JumpEvent += HandleJumpEvent;
    }

    public override void Exit()
    {
        _player.playerInput.JumpEvent -= HandleJumpEvent;
        base.Exit();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (_player.IsGround.Value)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
        
    }

    public void HandleJumpEvent()
    {
        if (_player.coyoteCount > 0)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Jump);
            _player.coyoteCount = 0;
        }
    }
    
  
}
