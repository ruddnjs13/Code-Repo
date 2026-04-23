using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float _moveAmount;
    [SerializeField] private float _moveTime;
    private Tween _tween;
    
    public void OpenDoor()
    {
        _tween = transform.DOLocalMoveY(transform.localPosition.y + _moveAmount, _moveTime);
    }
    public void CloseDoor()
    {
        _tween = transform.DOLocalMoveY(transform.localPosition.y - _moveAmount, _moveTime);
    }
}
