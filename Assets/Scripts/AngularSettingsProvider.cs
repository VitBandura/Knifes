using System.Collections;
using System.Collections.Generic;
using Events;
using SimpleEventBus.Disposables;
using UnityEngine;

public class AngularSettingsProvider : MonoBehaviour
{
    private const int FULL_DEGREE_ANGLE = 360;
   
    [SerializeField] private int _positionsCount;
    
    private CompositeDisposable _subscriptions;
    
    private float _angularStepInDeg;
    private float _angularStepInRad;
    private List<AngularUnit> _angularSettings = new();
   
    private void Start()
    {
        SetAngularSettings();
    }
   
    private void SetAngularSettings()
    {
        _angularStepInDeg = FULL_DEGREE_ANGLE / _positionsCount;
        _angularStepInRad = _angularStepInDeg * Mathf.Deg2Rad;

        for (var i = 0; i < _positionsCount; i++)
        {
            var currentAngleInRad = _angularStepInRad * i;
            var currentAngleCos = Mathf.Cos(currentAngleInRad);
            var currentAngleSin = Mathf.Sin(currentAngleInRad);

            var currentAngleInDeg = _angularStepInDeg * i;
            
            _angularSettings.Add(new AngularUnit(currentAngleCos, currentAngleSin, currentAngleInDeg));
        }
        
        EventStreams.GameEvents.Publish(new AngularSettingsCalculatedEvent(_angularSettings));
    }

}
