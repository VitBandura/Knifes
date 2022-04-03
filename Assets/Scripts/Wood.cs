using UnityEngine;

public class Wood : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward, _rotationSpeed);
    }
}
