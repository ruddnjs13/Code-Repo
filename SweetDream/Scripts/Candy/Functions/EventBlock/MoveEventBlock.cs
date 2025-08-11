using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveEventBlock : EventBlock
{
    [SerializeField] private Vector2 _moveDir;
    [SerializeField] private float _duration;

    public override void VoidEvent()
    {
        base.VoidEvent();
        EventFunc();
    }

    protected override void EventFunc()
    {
        Vector2 dir = _moveDir;
        if (_moveDir.x == 0)
        {
            dir = new Vector2(transform.position.x, _moveDir.y);
        }
        else if (_moveDir.y == 0)
        {
            dir = new Vector2(_moveDir.x, transform.position.y);
        }
        transform.DOMove(dir, _duration);
    }
}
