using System;
using Events;
using SimpleEventBus.Disposables;
using UnityEngine;

public class TargetExplosion : MonoBehaviour
{
    [SerializeField] private float _explosionForce;

    private PointEffector2D _pointEffector2D;
    private CompositeDisposable _subscriptions;

    private void Awake()
    {
        _pointEffector2D = GetComponent<PointEffector2D>();
        _pointEffector2D.forceMagnitude = 0;
        _subscriptions = new CompositeDisposable
        {
            EventStreams.GameEvents.Subscribe<TargetDestroyedEvent>(Explode)
        };
    }

    private void Explode(TargetDestroyedEvent targetDestroyedEvent)
    {
        gameObject.SetActive(true);
        _pointEffector2D.forceMagnitude = _explosionForce;
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
