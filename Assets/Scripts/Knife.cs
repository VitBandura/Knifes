using Events;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Wood>() != null)
        {
            EventStreams.GameEvents.Publish(new KnifeGetsIntoTargetEvent(gameObject));
        }
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Knife>() != null)
        {
            //TODO add variable
            Debug.Log("lose");
            _rigidbody2D.velocity = Vector2.down * 2;
            _rigidbody2D.angularVelocity = 360f * 5;
            
            EventStreams.GameEvents.Publish(new GameOverEvent());
        }
    }
    
}
