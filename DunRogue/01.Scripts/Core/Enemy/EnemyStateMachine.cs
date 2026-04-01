using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyStateMachine
{
    public EnemyState CurrentState { get; private set; }
    public Dictionary<Enum,EnemyState> StateDictionary = new Dictionary<Enum,EnemyState>();

    public Enemy _enemyBase;

    public void Initialize(Enum startState, Enemy enemy)
    {
        _enemyBase = enemy;
        CurrentState = StateDictionary[startState];
        CurrentState.Enter();
    }

    public void ChangeState(Enum newState, bool forceMode = false)
    {
        if (_enemyBase.canStateChangeable == false && forceMode == false) return;
        if (_enemyBase.isDead) return;

        CurrentState.Exit();
        CurrentState = StateDictionary[newState];
        CurrentState.Enter();
    }

    public void  AddState(Enum stateEnum, EnemyState enemyState)
    {
        StateDictionary.Add(stateEnum, enemyState);
    }
}
