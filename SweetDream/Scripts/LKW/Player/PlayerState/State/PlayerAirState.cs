using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player player,PlayerStateMachine stateMachine ,string animBoolName) : base(player,stateMachine ,animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        _player._material.friction = 0;
        _player.playerInput.JumpEvent += HandleJumpEvent;
        _player.canFlip = true;
    }

    private void HandleJumpEvent()
    {
        _player.bufferCount = _player.jumpBuffer;
    }

    public override void Exit()
    {
        _player.playerInput.JumpEvent -= HandleJumpEvent;
        _player._material.friction = 0.02f;
        base.Exit();
    }
    
    

    public override void StateUpdate()
    {
        base.StateUpdate();
        CalculateBufferTime();
        
        _player.SetMovement(new Vector2(_player.playerInput.Movement.x * _player._moveSpeed,_player.RbCompo.velocity.y));
        CalculateTime();
        ApplyExtraGravity();
    }

    private void CalculateBufferTime()
    {
        if (_player.bufferCount >= 0)
        {
            _player.bufferCount -= Time.deltaTime;
        }
    }
    
    private void CalculateTime()
    {
        if (!_player.IsGround.Value)
        {
            _player.timeInAir += Time.deltaTime;
        }
        else
        {
            _player.timeInAir = 0;
        }
    }

    private void ApplyExtraGravity()
    {
        if (_player.timeInAir > _player._gravityDelay)
        {
            _player.RbCompo.AddForce(Vector2.down * _player._extraGravity, ForceMode2D.Impulse);
            _player.timeInAir = 0f;
        }
    }
}
