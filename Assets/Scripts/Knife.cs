using Events;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;
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
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Knife>() != null)
        {
            Debug.Log("lose");
            _rigidbody2D.AddForce(Vector2.down * 500);
            _rigidbody2D.AddTorque(500);
        }
    }
}
