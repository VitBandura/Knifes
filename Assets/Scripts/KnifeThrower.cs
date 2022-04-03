using UnityEngine;

public class KnifeThrower : MonoBehaviour
{
    [SerializeField] private KnifePool _knifePool;
    [SerializeField] private float _knifeSpeed;

    private GameObject _knife;
    private Rigidbody2D _knifeRigidBody2D;

    private void Start()
    {
        PrepareKnife();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowKnife();
        }
    }

    private void PrepareKnife()
    {
        _knife = _knifePool.TakeKnifeFromPool();
        _knifeRigidBody2D = _knife.GetComponent<Rigidbody2D>();
        _knife.transform.position = transform.position;
        _knife.SetActive(true);
    }

    private void ThrowKnife()
    {
        _knifeRigidBody2D.velocity = Vector2.up * _knifeSpeed;
        PrepareKnife();
    }
    
}
