using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using SimpleEventBus.Disposables;
using UnityEngine;

public class KnifeInWoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _knifeStuckPrefab;
    [SerializeField] private Transform _parent;

    private CompositeDisposable _subscriptions;
    private int _knivesInWoodCount;
    private Vector3 _parentPosition;
    private float _radius;

    private void Awake()
    {
        InitializeSubscriptions();
        
        _radius = _parent.GetComponent<CircleCollider2D>().radius;
        _parentPosition = _parent.transform.position;
    }

    private void InitializeSubscriptions()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.GameEvents.Subscribe<RandomizerGeneratedValuesEvent>(GetKnivesInWoodCount)
        };
    }

    private void GetKnivesInWoodCount(RandomizerGeneratedValuesEvent eventData)
    {
        _knivesInWoodCount = eventData.KnivesInWoodCount;
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
