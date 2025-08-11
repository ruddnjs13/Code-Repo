 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ScreenColorFeedback : Feedback
{
    private Light2D _light;
    private Color _previousColor;
    public override void PlayFeedback()
    {
        if (_light == null)
        {
            _light = GameObject.Find("Light 2D").GetComponent<Light2D>();
        }
        _previousColor = _light.color;
        _light.color = Color.cyan;
    }

    public override void StopFeedback()
    {
        if (_light == null) return;
        _light.color = _previousColor;
    }
}
