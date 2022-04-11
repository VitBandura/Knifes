using SimpleEventBus.Events;
using UnityEngine;

namespace Events
{
    public class GameStartedEvent : EventBase
    {
        public int WoodDurability;
        public int KnifeCountUI;

        public GameStartedEvent(int woodDurability, int knifeCountUI)
        {
            WoodDurability = woodDurability;
            KnifeCountUI = knifeCountUI;
        }
    }
}