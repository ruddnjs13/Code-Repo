using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EventBlock : MonoBehaviour
{
    public virtual void VoidEvent()
    {
    }

    public virtual void IntEvent(int intValue)
    {
    }

    public virtual void FloatEvent(float floatValue)
    {
    }

    public virtual void BooleanEvent(bool booleanValue)
    {
    }

    protected abstract void EventFunc();
}
