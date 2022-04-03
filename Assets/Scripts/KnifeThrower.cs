using Events;
using SimpleEventBus.Disposables;
using UnityEngine;

public class KnifeThrower : MonoBehaviour
{
    [SerializeField] private KnifePool _knifePool;
    [SerializeField] private float _knifeSpeed;

    private GameObject _knife;
    private Rigidbody2D _knifeRigidBody2D;
    private CompositeDisposable _subscriptions;
    private bool IsReadyForThrowing; 

    private void Start()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.GameEvents.Subscribe<KnifeGetsIntoTargetEvent>(HandleKnifeHit)
        };
        PrepareKnife();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowKnife();
        }
    }
    
    private void HandleKnifeHit(KnifeGetsIntoTargetEvent obj)
    {
        PrepareKnife();
    }
    
    private void PrepareKnife()
    {
        _knife = _knifePool.TakeKnifeFromPool();
        _knifeRigidBody2D = _knife.GetComponent<Rigidbody2D>();
        _knife.transform.position = transform.position;
        _knife.SetActive(true);
        IsReadyForThrowing = true;
    }
    
    private void ThrowKnife()
    {
        if (IsReadyForThrowing)
        {
            _knifeRigidBody2D.velocity = Vector2.up * _knifeSpeed;
        }

        IsReadyForThrowing = false;
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
