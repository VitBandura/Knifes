using Events;
using SimpleEventBus.Disposables;
using UnityEngine;

public class Wood : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particlePrefab;
    [SerializeField] private GameObject _destroyedWood;
    
    private float _durability;
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
        
        InitializeSubscriptions();
    }

    private void InitializeSubscriptions()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.GameEvents.Subscribe<RandomizerGeneratedValuesEvent>(GetDurabilityValue),
            EventStreams.GameEvents.Subscribe<KnifeGetsIntoTargetEvent>(HandleKnifeHit)
        };
    }

    private void GetDurabilityValue(RandomizerGeneratedValuesEvent eventData)
    {
        _durability = eventData.WoodDurability;
    }

    private void HandleKnifeHit(KnifeGetsIntoTargetEvent eventData)
    {
        _particlesOfHarming.transform.position = transform.position;
        _particlesOfHarming.Play();
        _durability--;
        if (_durability <= 0)
        {
            var stuckKnives = GetComponentsInChildren<Transform>();
            for (var i = 1; i < stuckKnives.Length; i++)
            {
                stuckKnives[i].parent = _destroyedWood.transform;
                stuckKnives[i].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
            gameObject.SetActive(false);
            EventStreams.GameEvents.Publish(new TargetDestroyedEvent());
        }
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
