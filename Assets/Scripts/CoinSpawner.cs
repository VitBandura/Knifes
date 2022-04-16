using Events;
using SimpleEventBus.Disposables;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinSpawner : MonoBehaviour
{
    private const float ROTATION_OFFSET = -90;
    
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private CoinChanceSettings _coinChanceSettings;

    private CircleCollider2D _circleCollider2D;
    private CompositeDisposable _subscriptions;

    private void Awake()
    {
        _circleCollider2D = _coinPrefab.GetComponent<CircleCollider2D>();
        
        InitializeSubscriptions();
    }

    private void InitializeSubscriptions()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.GameEvents.Subscribe<AngularSettingsCalculatedEvent>(SpawnCoins)
        };
    }

    private bool ShouldSpawnCoin()
    {
        float randomValue = Random.Range(1, 100);
        return randomValue <= _coinChanceSettings.Chance;
    }

    private void SpawnCoins(AngularSettingsCalculatedEvent eventData)
    {
        if (!ShouldSpawnCoin())
        {
            return;
        }
        
        var maxIndex = eventData.AngularSettings.Count;
        var angleIndex = Random.Range(0, maxIndex);
        var angularUnit = eventData.AngularSettings[angleIndex];

        var parentPosition = _parent.transform.position;
        var radius = _parent.GetComponent<CircleCollider2D>().radius + _circleCollider2D.radius;
        var xPosition = radius * angularUnit.Cos + parentPosition.x;
        var yPosition = radius * angularUnit.Sin + parentPosition.y;
        var position = new Vector3(xPosition, yPosition);
            
        var rotation = Quaternion.Euler(0,0,eventData.AngularSettings[angleIndex].Angle 
                                            + ROTATION_OFFSET);

        Instantiate(_coinPrefab, position, rotation, _parent);

    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
