using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    private List<Feedback> _feedbacks;

    private void Awake()
    {
        _feedbacks = GetComponents<Feedback>().ToList();
    }

    public void PlayFeedback()
    {
        _feedbacks.ForEach(a => a.PlayFeedback());
    }

    public void StopFeedback()
    {
        _feedbacks.ForEach(a => a.StopFeedback());
    }
}
