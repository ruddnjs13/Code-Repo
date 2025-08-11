using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEffectFeedbackInCanvas : Feedback
{
    [SerializeField] private string _effectName;
    public override void PlayFeedback()
    {
        EffectPlayer effect = PoolManager.Instance.Pop(_effectName) as EffectPlayer;
        effect.SetPositionAndPlay(Camera.main.ScreenToWorldPoint(transform.position));
    }

    public override void StopFeedback()
    {
        
    }
}
