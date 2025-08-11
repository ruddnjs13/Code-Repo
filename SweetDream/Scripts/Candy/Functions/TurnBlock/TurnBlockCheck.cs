using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnBlockCheck : MonoBehaviour, IInteractable
{
    public UnityEvent OnCheck;

    public void Interact()
    {
        Debug.Log("구독!");
        SoundManager.Instance.PlaySfx(SFXEnum.Butten);
        OnCheck?.Invoke();
    }

    public void EndInteract()
    {
    }
}
