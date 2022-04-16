using Events;
using SimpleEventBus.Disposables;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinSpawner : MonoBehaviour
{
    private const float MIN_CHANCE = 1f;
    private const float MAX_CHANCE = 100f;
    private const float ROTATION_OFFSET = -90f;

    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private CoinChanceSettings _coinChanceSettings;

    private CompositeDisposable _subscriptions;

    private void Awake()
    {
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
       return Random.Range(MIN_CHANCE, MAX_CHANCE) <= _coinChanceSettings.Chance;
    }

    private void SpawnCoins(AngularSettingsCalculatedEvent eventData)
    {
        if (!ShouldSpawnCoin())
        {
            return;
        }
        
        var angularUnit = GetAngularUnit(eventData);
        var radius = CalculateRadius();

        var coinPosition = SetCoinPosition(radius, angularUnit);

        var coinRotation = Quaternion.Euler(0,0,angularUnit.Angle + ROTATION_OFFSET);

        Instantiate(_coinPrefab, coinPosition, coinRotation, _parent);

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

    private float CalculateRadius()
    {
        var prefabCollider = _coinPrefab.GetComponent<CircleCollider2D>();
        var parentCollider = _parent.GetComponent<CircleCollider2D>();
        
        return parentCollider.radius + prefabCollider.radius;
    }

    private Vector3 SetCoinPosition(float radius, AngularUnit angularUnit)
    {
        var parentPosition = _parent.transform.position;
        var xPosition = radius * angularUnit.Cos + parentPosition.x;
        var yPosition = radius * angularUnit.Sin + parentPosition.y;
        
        return new Vector3(xPosition, yPosition);
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
