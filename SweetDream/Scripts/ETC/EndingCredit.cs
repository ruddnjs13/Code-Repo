using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndingCredit : MonoBehaviour
{
    Sequence sequence;
    [SerializeField] private RectTransform _creditPanel;

    private readonly int height = 1600;

    private void Start()
    {
        sequence = DOTween.Sequence();

        sequence.AppendInterval(2f);
        sequence.Append(_creditPanel.DOAnchorPosY(_creditPanel.anchoredPosition.y+height,4, true));
        sequence.AppendInterval(2f);
        sequence.Append(_creditPanel.DOAnchorPosY(_creditPanel.anchoredPosition.y+ height*2,4, true));
        sequence.AppendInterval(2f);
        sequence.Append(_creditPanel.DOAnchorPosY(_creditPanel.anchoredPosition.y+height*3,4, true));
        sequence.AppendInterval(2f);
        sequence.Append(_creditPanel.DOAnchorPosY(_creditPanel.anchoredPosition.y+height*4,4, true));
        sequence.AppendInterval(2f);
        sequence.Append(_creditPanel.DOAnchorPosY(_creditPanel.anchoredPosition.y+height*5,4, true));
        sequence.AppendInterval(2f);
        sequence.Append(_creditPanel.DOAnchorPosY(_creditPanel.anchoredPosition.y+height*6,4, true));

    }
}
