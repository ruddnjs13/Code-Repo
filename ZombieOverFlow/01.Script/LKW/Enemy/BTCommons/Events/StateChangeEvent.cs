using Enemies;
using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/StateChangeEvent")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "StateChangeEvent", message: "enemy state change to [newState]", category: "Events", id: "9534157c026e0d09e9682f9168083fda")]
public partial class StateChangeEvent : EventChannelBase
{
    public delegate void StateChangeEventEventHandler(BtEnemyState newState);
    public event StateChangeEventEventHandler Event; 
    
    public void SendEventMessage(BtEnemyState newState)
    
    {
        Event?.Invoke(newState);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<BtEnemyState> newStateBlackboardVariable = messageData[0] as BlackboardVariable<BtEnemyState>;
        var newState = newStateBlackboardVariable != null ? newStateBlackboardVariable.Value : default(BtEnemyState);

        Event?.Invoke(newState);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        StateChangeEventEventHandler del = (newState) =>
        {
            BlackboardVariable<BtEnemyState> var0 = vars[0] as BlackboardVariable<BtEnemyState>;
            if(var0 != null)
                var0.Value = newState;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as StateChangeEventEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as StateChangeEventEventHandler;
    }
}

