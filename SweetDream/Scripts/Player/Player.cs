using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class Player : Agent
{
    public UnityEvent OnDeadEvent;
    
    [Header("MoveSetting")] 
    public float moveSpeed; 
    public float jumpPower;
    public float timeInAir = 0;
    public float extraGravity = 10;
    public float gravityDelay = 0.2f;

    public PhysicsMaterial2D material;
    
    [SerializeField] private InputReaderSO inputReader;

    public InputReaderSO playerInput => inputReader;

    public PlayerStateMachine StateMachine { get; private set; } = null;

    protected override void Awake()
    {
        base.Awake();
        InitStateMachine();
    }

    private void InitStateMachine()
    {
        StateMachine = new PlayerStateMachine();
        foreach (PlayerStateEnum stateEnum in Enum.GetValues(typeof(PlayerStateEnum)))
        {
            string typeName = stateEnum.ToString();

            try
            {
                Type type = Type.GetType($"Player{typeName}State");
                PlayerState state = Activator.CreateInstance(type, this, StateMachine, typeName) as PlayerState;
                StateMachine.AddState(stateEnum, state);
            }
            catch (Exception e)
            {
                Debug.LogError($"Player{typeName}State is not exist :");
                Debug.LogError(e);
            }
        }
    }

    private void Start()
    {
        StateMachine.Initialize(this, PlayerStateEnum.Idle);
    }

    private void OnEnable()
    {
        playerInput.OnMoveEvent += Flip;
    }

    private void OnDisable()
    {
        playerInput.OnMoveEvent -= Flip;
    }

    protected void Update()
    {
        CheckGround();
        StateMachine.CurrentState.StateUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            StateMachine.ChangeState(PlayerStateEnum.Dead);
        }
    }

    public void MovePlayer()
    {
        inputReader.RockInput(false);
    }
    public void StopPlayer()
    {
        inputReader.RockInput(true);
    }
}