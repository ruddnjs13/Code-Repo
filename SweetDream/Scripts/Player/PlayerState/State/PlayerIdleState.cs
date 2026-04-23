using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player player,PlayerStateMachine stateMachine ,string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    

    public override void Enter()
    {
        base.Enter();
        _player.StopImmediately(false);
    }


    public override void StateUpdate()
    {
        base.StateUpdate();

        float xMove = _player.playerInput.Movement.x;

        if (Mathf.Abs(xMove) > 0)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Move);
        }

        if (!_player.IsGround.Value)
        {
            _player.StateMachine.ChangeState(PlayerStateEnum.Fall);
        }
    }
}
