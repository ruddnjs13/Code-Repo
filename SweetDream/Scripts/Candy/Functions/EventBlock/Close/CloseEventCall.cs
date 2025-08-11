using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CloseEventCall : MonoBehaviour
{
    public UnityEvent OnEnter;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnEnter?.Invoke();
        }
    }
}
