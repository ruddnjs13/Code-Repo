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
    #region MoveSettingRegion
    [Header("MoveSetting")] 
    public float _moveSpeed; 
    #endregion

    #region JumpSetting
    public float _jumpPower;
    public float timeInAir = 0;
    public float _extraGravity = 10;
    public float _gravityDelay = 0.2f;
    #endregion

    [field:SerializeField]public PhysicsMaterial2D _material { get; set; }

    [SerializeField] private InputReaderSO _inputReader;
    public InputReaderSO playerInput => _inputReader;

    public UnityEvent OnDeadEvent;
    
    public PlayerStateMachine StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
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
        playerInput.JumpEvent += HandleJumpEvent;
    }

    private void OnDisable()
    {
        playerInput.OnMoveEvent -= Flip;
        playerInput.JumpEvent = HandleJumpEvent;
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
        _inputReader.RockInput(false);
    }
    public void StopPlayer()
    {
        _inputReader.RockInput(true);
    }
}