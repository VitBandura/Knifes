using Events;
using SimpleEventBus.Disposables;
using UnityEngine;

public class KnifeInWoodSpawner : MonoBehaviour
{
    private const float MAX_STUCK_KNIVES_COUNT = 3f;
    private const float MIN_STUCK_KNIVES_COUNT = 0.1f;
    private const float ROTATION_OFFSET = 90f;
    
    [SerializeField] private GameObject _knifeStuckPrefab;
    [SerializeField] private Transform _parent;

    private CompositeDisposable _subscriptions;
    
    private void Awake()
    {
        InitializeSubscriptions();
    }

    private void InitializeSubscriptions()
    {
        _subscriptions = new CompositeDisposable
        {
           EventStreams.GameEvents.Subscribe<AngularSettingsCalculatedEvent>(SpawnKnivesInWood)
        };
    }
    
    private float GenerateStuckKnivesCount()
    {
       return Random.Range(MIN_STUCK_KNIVES_COUNT, MAX_STUCK_KNIVES_COUNT);
    }
    
    private void SpawnKnivesInWood(AngularSettingsCalculatedEvent eventData)
    {
        var knivesCount= GenerateStuckKnivesCount();

        for (var i = 0; i < knivesCount; i++)
        {
            var angularUnit = GetAngularUnit(eventData);
            var radius = _parent.GetComponent<CircleCollider2D>().radius;

            var knifePosition = SetKnifePositionInWood(radius, angularUnit);

            var knifeRotation = Quaternion.Euler(0,0,angularUnit.Angle + ROTATION_OFFSET);

            Instantiate(_knifeStuckPrefab, knifePosition, knifeRotation, _parent);
        }
    }

    private AngularUnit GetAngularUnit(AngularSettingsCalculatedEvent eventData)
    {
        var maxIndex = eventData.AngularSettings.Count;
        var minIndex = 0;
        var angleIndex = Random.Range(minIndex, maxIndex);
        var angularUnit = eventData.AngularSettings[angleIndex];
        eventData.AngularSettings.RemoveAt(angleIndex);
        return angularUnit;
    }

    private Vector3 SetKnifePositionInWood(float radius, AngularUnit angularUnit)
    {
        var parentPosition = _parent.transform.position;
        var xPosition = radius * angularUnit.Cos + parentPosition.x;
        var yPosition = radius * angularUnit.Sin + parentPosition.y;
        var knifePosition = new Vector3(xPosition, yPosition);
        return knifePosition;
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
