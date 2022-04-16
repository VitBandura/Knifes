using System.Collections;
using System.Collections.Generic;
using Events;
using SimpleEventBus.Disposables;
using UnityEngine;

public class AppleDestructor : MonoBehaviour
{
    [SerializeField] private float _maxDestroyedPartSpeed = 300f;
    [SerializeField] private float _minDestroyedPartSpeed = 150f;
    [SerializeField] private float _maxDestroyedPartTorque = 400f;
    [SerializeField] private float _minDestroyedPartTorque = -400f;

    private Rigidbody2D[] _parts;
    private CompositeDisposable _subscriptions;
    
    private void Awake()
    {
        gameObject.SetActive(false);
        InitializeSubscriptions();
    }

    private void OnEnable()
    {
        ExplodeTargetParts();
    }

    private void InitializeSubscriptions()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.GameEvents.Subscribe<AppleDestroyedEvent>(HandleAppleDestruction)
        };
    }
    
    private void HandleAppleDestruction(AppleDestroyedEvent eventData)
    {
        transform.position = eventData.Apple.transform.position;
        gameObject.SetActive(true);
    }

    private void ExplodeTargetParts()
    {
        GetAllPartsOfDestroyedTarget();
        foreach (var part in _parts)
        {
            var destroyedPartSpeed = Random.Range(_minDestroyedPartSpeed, _maxDestroyedPartSpeed);
            var destroyedPartTorque = Random.Range(_minDestroyedPartTorque, _maxDestroyedPartTorque);
            var forceUnit = Vector2.up * destroyedPartSpeed;
            part.AddForce(forceUnit);
            part.AddTorque(destroyedPartTorque);
        }
    }
   
    private void GetAllPartsOfDestroyedTarget()
    {
        _parts = GetComponentsInChildren<Rigidbody2D>();
    }
   
    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
