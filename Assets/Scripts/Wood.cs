using Events;
using SimpleEventBus.Disposables;
using Unity.Mathematics;
using UnityEngine;

public class Wood : MonoBehaviour
{
    [SerializeField] private float _durability;
    [SerializeField] private ParticleSystem _particlePrefab;

    
    private CompositeDisposable _subscriptions;
    private ParticleSystem _particlesOfHarming;
    private CircleCollider2D _circleCollider2D;
    private Vector3 _particlesPosition;
    private Vector3 _particlesPositionOffset;

    private void Awake()
    {
        _circleCollider2D = GetComponent<CircleCollider2D>();
        
        _particlesPositionOffset = new Vector3(0, _circleCollider2D.radius, 0);
        _particlesPosition = transform.position - _particlesPositionOffset;
        
        _particlesOfHarming = Instantiate(_particlePrefab);
        _particlesOfHarming.transform.position = _particlesPosition;
        
        _subscriptions = new CompositeDisposable
        {
            EventStreams.GameEvents.Subscribe<KnifeGetsIntoTargetEvent>(HandleKnifeHit)
        };
    }

    private void HandleKnifeHit(KnifeGetsIntoTargetEvent obj)
    {
        _particlesOfHarming.transform.position = transform.position;
        _particlesOfHarming.Play();
        _durability--;
        if (_durability <= 0)
        {
            EventStreams.GameEvents.Publish(new TargetDestroyedEvent());
        }
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
