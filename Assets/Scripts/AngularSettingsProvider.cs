using System.Collections.Generic;
using Events;
using SimpleEventBus.Disposables;
using UnityEngine;

public class AngularSettingsProvider : MonoBehaviour
{
    private const int FULL_DEGREE_ANGLE = 360;
   
    private CompositeDisposable _subscriptions;
    private int _positionsCount;
  
    private float _angularStepInDeg;
    private float _angularStepInRad;
    private List<AngularUnit> _angularSettings = new();
   
    private void Awake()
    {
        InitializeSubscriptions();
    }
    
    private void InitializeSubscriptions()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.GameEvents.Subscribe<RandomizerGeneratedValuesEvent>(SetAngularSettings)
        };
    }

    private void SetAngularSettings(RandomizerGeneratedValuesEvent eventData)
    {
        _positionsCount = eventData.CircularSpawningPointsCount;
     
        _angularStepInDeg = FULL_DEGREE_ANGLE / _positionsCount;
        _angularStepInRad = _angularStepInDeg * Mathf.Deg2Rad;

        for (var i = 0; i < _positionsCount; i++)
        {
            var currentAngleInRad = _angularStepInRad * i;
            var currentAngleCos = Mathf.Cos(currentAngleInRad);
            var currentAngleSin = Mathf.Sin(currentAngleInRad);

            var currentAngleInDeg = _angularStepInDeg * i;
            var rotation = Quaternion.Euler(0, 0, currentAngleInDeg);

            _angularSettings.Add(new AngularUnit(currentAngleCos, currentAngleSin, rotation));
        }
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
