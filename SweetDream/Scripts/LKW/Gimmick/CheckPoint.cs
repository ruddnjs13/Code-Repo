using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class CheckPoint : MonoBehaviour
{
    public UnityEvent OnEnableEvent;
    public int rotateIdx = 0;
    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Light2D _spotLight;
    
    private SpriteRenderer _spriteRenderer;

    public bool isActive { get; private set; } = false;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        rotateIdx = (int)transform.rotation.eulerAngles.z / 90;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isActive) return;
            SoundManager.Instance.PlaySfx(SFXEnum.Save);
            GameManager.Instance.EnableCheckPoint(this);
            _spriteRenderer.sprite = _onSprite;
            _spotLight.gameObject.SetActive(true);
            OnEnableEvent.Invoke();
            isActive = true;
        }
    }
}
