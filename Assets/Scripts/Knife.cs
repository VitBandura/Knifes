using System;
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
            _rigidbody2D.velocity = Vector2.zero;
            transform.parent = other.transform;
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
