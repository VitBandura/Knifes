using SimpleEventBus.Events;
using UnityEngine;

namespace Events
{
    public class AppleDestroyedEvent : EventBase
    {
        public GameObject Apple { get; }

        public AppleDestroyedEvent(GameObject apple)
        {
            Apple = apple;
        }
    }
}