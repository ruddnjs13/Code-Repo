using DewmoLib.Utiles;

namespace _00Work.LKW.Code.Events
{
    public class StageEvent
    {
        public static GetHeartEvent GetHeartEvent = new GetHeartEvent();
        public static EnableGoalEvent EnableGoalEvent = new EnableGoalEvent();
        public static PlayerGoalEvent PlayerGoalEvent = new PlayerGoalEvent();
    }

    public class GetHeartEvent : GameEvent
    {
    };

    public class EnableGoalEvent : GameEvent
    {
    };

    public class PlayerGoalEvent : GameEvent
    {
    };
}