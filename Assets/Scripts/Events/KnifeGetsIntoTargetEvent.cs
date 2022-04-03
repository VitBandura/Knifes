using SimpleEventBus.Events;
using UnityEngine;

namespace Events
{
    public class KnifeGetsIntoTargetEvent : EventBase
    {
        public GameObject Knife { get; }

        public KnifeGetsIntoTargetEvent(GameObject knife)
        {
            Knife = knife;
        }
    }
}