using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InitializeEnterTrigger : MonoBehaviour
{
    private List<InitializeManager> initializeManagers;

    private void Awake()
    {
        initializeManagers = GetComponentsInChildren<InitializeManager>().ToList();
    }

    public void Initialize()
    {
        foreach (var initializeManager in initializeManagers)
        {
            initializeManager.gameObject.SetActive(true);
        }
    }
}
