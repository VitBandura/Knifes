using System.Collections;
using Events;
using SimpleEventBus.Disposables;
using UnityEngine;

public class Wood : MonoBehaviour
{
    private const float ShakingDuration = 0.05f;
    private readonly Vector3 _shakingOffset = new (0, 0.08f);
    
    [SerializeField] private float _rotationSpeed;

    private CompositeDisposable _subscriptions;
    
    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.GameEvents.Subscribe<KnifeGetsIntoTargetEvent>(ShakeTarget)
        };
    }

    private void ShakeTarget(KnifeGetsIntoTargetEvent obj)
    {
        StartCoroutine(Shake());
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward, _rotationSpeed);
    }

    private IEnumerator Shake()
    {
        transform.position += _shakingOffset;
        yield return new WaitForSeconds(ShakingDuration);
        transform.position -= _shakingOffset;
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
