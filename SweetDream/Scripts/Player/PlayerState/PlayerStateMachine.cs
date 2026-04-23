using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public enum PlayerStateEnum
{
    Air,
    Idle,
    Move,
    Jump,
    Fall,
    Dead
}

public class PlayerStateMachine
{
    public PlayerState CurrentState { get; private set; }
    public Dictionary<PlayerStateEnum, PlayerState> stateDictionary;

    private Player _player;

    public PlayerStateMachine()
    {
        stateDictionary = new Dictionary<PlayerStateEnum, PlayerState>();
    }

    public void Initialize(Player player, PlayerStateEnum startStateEnum)
    {
        _player = player;
        CurrentState = stateDictionary[startStateEnum];
        CurrentState.Enter();
    }

    public void ChangeState(PlayerStateEnum nextState)
    {
        CurrentState.Exit();
        CurrentState = stateDictionary[nextState];
        CurrentState.Enter();
    }

    public void AddState(PlayerStateEnum stateEnum, PlayerState playerState)
    {
        stateDictionary.Add(stateEnum, playerState);
    }
}
