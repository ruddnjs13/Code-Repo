using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseEventBlock : EventBlock
{
    public override void VoidEvent()
    {
        base.VoidEvent();
        EventFunc();
    }

    protected override void EventFunc()
    {
        gameObject.SetActive(true);
    }
}
