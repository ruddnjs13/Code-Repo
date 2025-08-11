using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.Serialization;

public class TurnBlock : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite _sprite;
    
    //회전 배열 (순서대로)
    private int[] _rotateArr = { 90, 180, 270, 0 };
    
    // 배열을 하나하나 돌려줄 인덱스값 (프로퍼티)
    [SerializeField] private int _rotateIdx = 0;

    private Collider2D _collider;


    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    public int RotateIdx
    {
        get => _rotateIdx;

        set
        {
            if (value < 0) _rotateIdx = 0;
            else if (value >= _rotateArr.Length) _rotateIdx = 0;
            else _rotateIdx = value;
        }
    }
    
    // 정답 체크 해줄 부울값
    public bool IsCorrect { get; private set; } = false;
    
    public void Initialize()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
    
    
    // 정답인 값을 받아옴.
    [SerializeField] private Transform _correctTrm;
    
    // 보간 때 사용할 간격
    [SerializeField] private float _duration = 10f;

    // 입력을 받았을 때에 실행될 이벤트 만들기 (상호작용 시에 호출될 이벤트)
    public UnityEvent OnInputEvent;
    // 스프라이트를 변경시켜줄 이벤트
    public UnityEvent<Sprite> OnChangeSprite;
    
    // 실행될 트윈 및 시퀀스 선언
    private Tween _tween;
    private Sequence _sequence;
    
    // 이벤트 구독 및 해제
    private void OnEnable()
    {
        OnInputEvent.AddListener(HandleTurnEvent);
    }

    private void OnDisable()
    {
        OnInputEvent.RemoveListener(HandleTurnEvent);
    }

    // 구독받을 메서드 만들기
    public void HandleTurnEvent()
    {
        try
        {
            _collider.enabled = false;
        }
        catch (Exception e)
        {
            Debug.LogError($"Collider2D Load ERROR\n ERROR Name{e.Message}");
        }
        
        //회전 기능
        _tween = transform.DOLocalRotate(new Vector3(0, 0, Mathf.Approximately(_rotateArr[_rotateIdx], transform.localRotation.eulerAngles.z) ? _rotateArr[++_rotateIdx] : _rotateArr[_rotateIdx]), _duration);
        // 기능을 작동시켜줄 시퀀스.
        _sequence = DOTween.Sequence();
        _sequence.Append(_tween);
    
        _sequence.OnComplete(() => Debug.Log("<color=blue>시퀀스 끝</color>"));

        // 시퀀스가 끝났을 때에 정답 확인
        _sequence.OnComplete(() =>
        {
            if (Mathf.Approximately(_correctTrm.localRotation.eulerAngles.z, _rotateArr[_rotateIdx]))
            {
                Debug.Log("일치");
                IsCorrect = true;
                OnChangeSprite?.Invoke(_sprite);
            }
            _collider.enabled = true;
            RotateIdx++;
        });
    }

    public void Interact()
    {
        Debug.Log("나 인풋받음");
        if (_sequence.IsActive() || IsCorrect) return;
        SoundManager.Instance.PlaySfx(SFXEnum.RotateArrow);
        OnInputEvent?.Invoke();
    }

    public void EndInteract()
    {
    }
}
