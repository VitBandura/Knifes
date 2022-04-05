using Events;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
    private Vector2 _stuckKnifeColliderSize;
    private Vector2 _stuckKnifeColliderOffset;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _stuckKnifeColliderSize = new Vector2(_boxCollider2D.size.x, 1);
        _stuckKnifeColliderOffset = new Vector2(_boxCollider2D.offset.x, -0.25f);
    }

   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Wood>() != null)
        {
            GetStuckInTarget(other);
            EventStreams.GameEvents.Publish(new KnifeGetsIntoTargetEvent(gameObject));
        }
    }
   

    private void GetStuckInTarget(Collider2D other)
    {
        _rigidbody2D.velocity = Vector2.zero;
        transform.parent = other.transform;
        _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        _boxCollider2D.size = _stuckKnifeColliderSize;
        _boxCollider2D.offset = _stuckKnifeColliderOffset;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Knife>() != null)
        {
            //TODO add variable
            Debug.Log("lose");
            _rigidbody2D.velocity = Vector2.down * 2;
            _rigidbody2D.angularVelocity = 360f * 5;
        }
    }
}
