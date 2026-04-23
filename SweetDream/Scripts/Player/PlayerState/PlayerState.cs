using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.Video;

public abstract class PlayerState
{
    protected Player _player;
    protected readonly int _animBoolHash;
    protected PlayerStateMachine _stateMachine;
    protected string _animName;

    protected PlayerState(Player player,PlayerStateMachine stateMachine,string animBoolName)
    {
        _player = player;
        _animBoolHash = Animator.StringToHash(animBoolName);
        _stateMachine = stateMachine;
        _animName = animBoolName;
    }

    public virtual void  Enter()
    {
        _player.AnimatorCompo.SetBool(_animBoolHash,true);
    }

    public virtual void Exit()
    {
        _player.AnimatorCompo.SetBool(_animBoolHash,false);
    }

    public virtual void StateUpdate()
    {
        
    }
}
