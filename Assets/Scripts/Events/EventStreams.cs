using SimpleEventBus;
using SimpleEventBus.Interfaces;
using UnityEngine;

public class EventStreams : MonoBehaviour
{
    public static IEventBus GameEvents { get; } = new EventBus();
}
