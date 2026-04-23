using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.InputSystem;

public class TutorialTextMove : MonoBehaviour
{
    private TextMeshPro _textMeshPro;
    private Controls _controls;

    private void Awake()
    {
        _controls = new Controls();
        _textMeshPro = GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        _textMeshPro.text = $"{_controls.Player.Movement.GetBindingDisplayString(3)}키와 " +
                            $"{_controls.Player.Movement.GetBindingDisplayString(4)}키를 눌러 \n좌우로 이동할 수 있습니다";
        transform.DOLocalMoveY(transform.position.y -0.4f, 0.5f)
            .SetEase(Ease.OutCirc)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
