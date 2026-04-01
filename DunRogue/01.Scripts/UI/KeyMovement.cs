using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class KeyMovement : MonoBehaviour
{
    private void Start()
    {
        transform.DOMoveY(transform.position.y + 0.4f,0.6f)
            .SetEase(Ease.OutCirc)
            .SetLoops(10000,LoopType.Yoyo);
    }
}
