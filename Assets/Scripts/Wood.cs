using System;
using UnityEngine;

public class Wood : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private void FixedUpdate()
    {
        transform.Rotate(0,0, _rotationSpeed);
    }
}
