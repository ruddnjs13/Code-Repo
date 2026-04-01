using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class AgentMove : MonoBehaviour
{
    #region Component
    public Rigidbody2D rbCompo { get; private set; }
    #endregion 
    [Header("MoveSettring")]
    #region
    [SerializeField] private float _moveSpeed = 10f;
    #endregion

    private Vector2 movement;

    private bool isStop = false;

    private void Awake()
    {
        rbCompo = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        if (!isStop)
        {
            rbCompo.velocity = movement * _moveSpeed;
        }

    }

    public void Stop()
    {
        rbCompo.velocity = Vector2.zero;
        isStop = true;
    }
    public void Move()
    {
        isStop = false;
    }

    public void SetMovement(Vector2 moveMent)
    {
        this.movement = moveMent;
    }

    public void MoveEnemy(Vector3 target)
    {
        Vector2 moveDir = (target - transform.position).normalized;
        rbCompo.velocity = moveDir * _moveSpeed;
    }


    public void StopImmediately(bool isYStop)
    {
        if (isYStop)
        {
            rbCompo.velocity = Vector2.zero;
        }
        else return;
    }
}
