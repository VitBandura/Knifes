using Events;
using SimpleEventBus.Disposables;
using Unity.Mathematics;
using UnityEngine;

public class Wood : MonoBehaviour
{
    [SerializeField] private float _durability;
    [SerializeField] private ParticleSystem _harmParticles;
    
    private CompositeDisposable _subscriptions;

    private void Awake()
    {
        
        _subscriptions = new CompositeDisposable
        {
            EventStreams.GameEvents.Subscribe<KnifeGetsIntoTargetEvent>(HandleKnifeHit)
        };
    }

    private void HandleKnifeHit(KnifeGetsIntoTargetEvent obj)
    {
        Instantiate(_harmParticles, transform.position, quaternion.identity);
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
