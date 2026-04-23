using Cinemachine;
using UnityEngine;


[RequireComponent(typeof(CinemachineImpulseSource))]
public class ImpulseFeedback : Feedback
{
    [SerializeField] private float _impulseForce = 0;
    private CinemachineImpulseSource _source;

    private void Awake()
    {
        _source = GetComponent<CinemachineImpulseSource>();
    }

    public override void PlayFeedback()
    {
        _source.GenerateImpulse(_impulseForce);
    }

    public override void StopFeedback()
    {
        
    }
}
