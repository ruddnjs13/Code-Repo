using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour
{
    private void Start()
    {
        transform.DOLocalMoveX(transform.localPosition.x + 4, 0.5f)
            .SetLoops(-1,LoopType.Yoyo);
    }
}
