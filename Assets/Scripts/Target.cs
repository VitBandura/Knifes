using System.Collections;
using Events;
using SimpleEventBus.Disposables;
using UnityEngine;

public class Target : MonoBehaviour
{
    private const float SHAKING_DURATION = 0.05f;
    private readonly Vector3 _shakingOffset = new (0, 0.08f);
    
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _durability;

    private CompositeDisposable _subscriptions;
    
    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.GameEvents.Subscribe<KnifeGetsIntoTargetEvent>(HandleKnifeHit),
        };
    }
    
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward, _rotationSpeed);
    }

    private void HandleKnifeHit(KnifeGetsIntoTargetEvent obj)
    {
        _durability--;
        if (_durability <= 0)
        {
            DestroyTarget();
            return;
        }
        StartCoroutine(ShakeTarget());
    }
    
    private IEnumerator ShakeTarget()
    {
        transform.position += _shakingOffset;
        yield return new WaitForSeconds(SHAKING_DURATION);
        transform.position -= _shakingOffset;
    }

    private void DestroyTarget()
    {
        
    }
    
    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
