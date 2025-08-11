using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InitializeObject : MonoBehaviour
{
    protected Vector3 _originLocalPos = Vector3.zero;
    protected Quaternion _originLocalRot = Quaternion.identity;

    protected virtual void Awake()
    {
        _originLocalPos = transform.position;
        _originLocalRot = transform.localRotation;
    }

    public virtual void Initialize()
    {
        transform.localPosition = _originLocalPos;
        transform.localRotation = _originLocalRot;
    }
}
