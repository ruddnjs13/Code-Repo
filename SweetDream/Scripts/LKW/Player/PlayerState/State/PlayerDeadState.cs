using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        
    }


    public override void Enter()
    {
        _player.StopImmediately(true);
        _player.playerInput.RockInput(true);
        _player.OnDeadEvent?.Invoke();
    }

    public override void Exit()
    {
        _player.playerInput.RockInput(false);
        base.Exit();
    }
}
