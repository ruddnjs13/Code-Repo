using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class FastDeadBlockDot : MonoBehaviour
{
    public UnityEvent OnHit;

    public bool isCanSoundActive = false;

    [SerializeField] private float _position;
    [SerializeField] private float _delayUpSecond;
    
    public float _duration;

    private Tween _tween, _tween2;
    private Sequence _sequence;
    private Transform _pos;

    
    public void Start()
    {
        SetSequence();
    }
    
    public void SetSequence()
    {
        _tween = transform.DOLocalMove(new Vector2(transform.localPosition.x, _position), _duration)
            .SetEase(Ease.OutQuart)
            .OnComplete(() =>
            {
                if(isCanSoundActive) SoundManager.Instance.PlaySfx(SFXEnum.Smahser);
            });

        _sequence = DOTween.Sequence();
        _sequence.AppendInterval(_delayUpSecond)
            .Append(_tween)
            .AppendInterval(_delayUpSecond);
        _sequence.SetLoops(-1, LoopType.Yoyo);
    }

    public void ChangeSoundSetting(bool setting)
    {
        isCanSoundActive = setting;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer ==LayerMask.NameToLayer("Player"))
        {
            OnHit?.Invoke();
        }
    }
}