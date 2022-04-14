using System.Collections;
using Events;
using SimpleEventBus.Disposables;
using UnityEngine;

public class KnifeThrower : MonoBehaviour
{
    [SerializeField] private KnifePool _knifePool;
    [SerializeField] private float _knifeSpeed;
    [SerializeField] private float _delayTime;
    
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
        var knife = _knifePool.TakeKnifeFromPool();
        var throwingPosition = transform.position;
        
        _knifeRigidBody2D = knife.GetComponent<Rigidbody2D>();
        knife.transform.position = throwingPosition;
        knife.SetActive(true);
        _isReadyForThrowing = true;
    }
    
    private void ThrowKnife()
    {
        if (_isReadyForThrowing)
        {
            var throwingVelocity = Vector2.up * _knifeSpeed;
            _knifeRigidBody2D.velocity = throwingVelocity;
            _isReadyForThrowing = false;
            
            EventStreams.GameEvents.Publish(new KnifeWasThrownEvent());
        }
    }

    private void HandleKnifeHit(KnifeGetsIntoTargetEvent eventData)
    {
        if (!_isTargetDestroyed)
        {
            StartCoroutine(PrepareKnifeAfterDelay());
        }
    }

    private IEnumerator PrepareKnifeAfterDelay()
    {
        yield return new WaitForSeconds(_delayTime);
        PrepareKnife();
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
