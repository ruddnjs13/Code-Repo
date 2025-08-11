using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeManager : MonoBehaviour
{
    public List<InitializeObject> initializeObjects;

    public void InitializeObjects()
    {
        foreach (InitializeObject item in initializeObjects)
        {
            item.Initialize();
        }
    }
}
