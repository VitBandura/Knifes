using SimpleEventBus.Events;

namespace Events
{
    public class GameStartedEvent : EventBase
    {
        public int WoodDurability { get; }
        public int KnifeCountUI { get; }

        public GameStartedEvent(int woodDurability, int knifeCountUI)
        {
            WoodDurability = woodDurability;
            KnifeCountUI = knifeCountUI;
        }
    }
}