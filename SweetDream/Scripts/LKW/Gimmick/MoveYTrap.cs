using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveYTrap : MonoBehaviour
{
    [SerializeField] private float moveYDistance = 2f;
    [SerializeField] private float moveDuration = 1.5f;
    
    private Sequence _moveSequence;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _moveSequence = DOTween.Sequence();
        _moveSequence.Append(transform.DOLocalMoveY(transform.localPosition.y + moveYDistance
            , moveDuration,false).SetEase(Ease.Linear));
        _moveSequence.Append(transform.DOLocalMoveY(transform.localPosition.y
            , moveDuration, false).SetEase(Ease.Linear));
        _moveSequence.AppendInterval(0.2f);
        _moveSequence.SetLoops(-1);
    }

    private void OnDisable()
    {
        this.transform.DOKill();
    }
}