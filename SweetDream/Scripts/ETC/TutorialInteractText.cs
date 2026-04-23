using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.InputSystem;

public class TutorialInteractText : MonoBehaviour
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
        _textMeshPro.text = $"{_controls.Player.Interaction.GetBindingDisplayString(0)}키를 눌러 사물과 상호작용 할 수 있습니다";
        transform.DOLocalMoveY(transform.position.y -0.4f, 0.5f)
            .SetEase(Ease.OutCirc)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}