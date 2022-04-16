using Events;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Knife>() != null)
        {
            gameObject.SetActive(false);
            EventStreams.GameEvents.Publish(new AppleDestroyedEvent(gameObject));
        }
    }
}
