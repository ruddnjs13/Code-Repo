using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    private SpriteRenderer _sprRenderer;

    private void Awake()
    {
        _sprRenderer = GetComponent<SpriteRenderer>();
    }

    public void Change(Sprite sprite)
    {
        _sprRenderer.sprite = sprite;
    }
}
