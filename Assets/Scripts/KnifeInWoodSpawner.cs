using Events;
using SimpleEventBus.Disposables;
using UnityEngine;

public class KnifeInWoodSpawner : MonoBehaviour
{
    private const int MAX_STUCK_KNIVES_COUNT = 4;
    private const int MIN_STUCK_KNIVES_COUNT = 1;
    private const float ROTATION_OFFSET = 90;
    
    [SerializeField] private GameObject _knifeStuckPrefab;
    [SerializeField] private Transform _parent;

    private CompositeDisposable _subscriptions;
    private int _knivesInWoodCount;
    private Vector3 _parentPosition;
    private float _radius;

    private void Awake()
    {
        _radius = _parent.GetComponent<CircleCollider2D>().radius;
        _parentPosition = _parent.transform.position;
        InitializeSubscriptions();
    }

    private void InitializeSubscriptions()
    {
        _subscriptions = new CompositeDisposable
        {
           EventStreams.GameEvents.Subscribe<AngularSettingsCalculatedEvent>(SpawnKnivesInWood)
        };
    }
    
    private void GenerateStuckKnifeCount()
    {
        _knivesInWoodCount = Random.Range(MAX_STUCK_KNIVES_COUNT, MIN_STUCK_KNIVES_COUNT);
    }
    private void SpawnKnivesInWood(AngularSettingsCalculatedEvent eventData)
    {
        GenerateStuckKnifeCount();
            
        for (var i = 0; i < _knivesInWoodCount; i++)
        {
            var maxIndex = eventData.AngularSettings.Count;
            int angleIndex = Random.Range(0, maxIndex);
            var angularUnit = eventData.AngularSettings[angleIndex];

            var xPosition = _radius * angularUnit.Cos + _parentPosition.x;
            var yPosition = _radius * angularUnit.Sin + _parentPosition.y;
            var position = new Vector3(xPosition, yPosition);
            
            var rotation = Quaternion.Euler(0,0,eventData.AngularSettings[angleIndex].Angle 
                                                + ROTATION_OFFSET);

            Instantiate(_knifeStuckPrefab, position, rotation, _parent);
        }
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
