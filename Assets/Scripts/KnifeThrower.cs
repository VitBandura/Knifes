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
    private bool _isReadyForThrowing;
    private bool _isTargetDestroyed;

    private void Start()
    {
        InitializeSubscriptions();
        PrepareKnife();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowKnife();
        }
    }
    
    private void InitializeSubscriptions()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.GameEvents.Subscribe<KnifeGetsIntoTargetEvent>(HandleKnifeHit),
            EventStreams.GameEvents.Subscribe<TargetDestroyedEvent>(HandleTargetDestruction)
        };
    }
    private void PrepareKnife()
    {
        _knife = _knifePool.TakeKnifeFromPool();
        _knifeRigidBody2D = _knife.GetComponent<Rigidbody2D>();
        _knife.transform.position = transform.position;
        _knife.SetActive(true);
        _isReadyForThrowing = true;
    }
    
    private void ThrowKnife()
    {
        if (_isReadyForThrowing)
        {
            _knifeRigidBody2D.velocity = Vector2.up * _knifeSpeed;
            _knifeRigidBody2D.gravityScale = 1;
            _isReadyForThrowing = false;
            
            EventStreams.GameEvents.Publish(new KnifeWasThrownEvent());
        }
    }

    private void HandleKnifeHit(KnifeGetsIntoTargetEvent eventData)
    {
        if (!_isTargetDestroyed)
        {
            PrepareKnife();
        }
    }
    
    private void HandleTargetDestruction(TargetDestroyedEvent eventData)
    {
        _isTargetDestroyed = true;
    }
    
    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
