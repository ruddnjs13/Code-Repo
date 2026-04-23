using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.InputSystem;

public class TutorialTextJump : MonoBehaviour
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
        _textMeshPro.text = $"{_controls.Player.Jump.GetBindingDisplayString(0)}키를 눌러 점프를 해\n 구덩이를 뛰어넘으세요";
        transform.DOLocalMoveY(transform.position.y -0.4f, 0.5f)
            .SetEase(Ease.OutCirc)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}