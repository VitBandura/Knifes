using Events;
using SimpleEventBus.Disposables;
using UnityEngine;

public class StuckKnifeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _stuckKnifePrefab;
    [SerializeField] private Transform _parent;

    private Vector3 _spawnPoint;
    private CompositeDisposable _subscriptions;

    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.GameEvents.Subscribe<KnifeGetsIntoTargetEvent>(SpawnStuckKnife)
        };

        _spawnPoint = new Vector3(_parent.transform.position.x, 
            _parent.transform.position.y - _parent.GetComponent<CircleCollider2D>().radius);
    }

    private void SpawnStuckKnife(KnifeGetsIntoTargetEvent eventData)
    {
        Instantiate(_stuckKnifePrefab, _spawnPoint, Quaternion.Euler(0,0,180), _parent);
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
