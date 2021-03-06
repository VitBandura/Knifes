using Events;
using SimpleEventBus.Disposables;
using UnityEngine;

public class StuckKnifeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _knifeStuckPrefab;
    [SerializeField] private Transform _parent;

    private Vector3 _spawnPoint;
    private CompositeDisposable _subscriptions;

    private void Awake()
    {
       _subscriptions = new CompositeDisposable
        {
            EventStreams.GameEvents.Subscribe<KnifeGetsIntoTargetEvent>(SpawnStuckKnife)
        };

       //TODO rework this 
        _spawnPoint = new Vector3(_parent.transform.position.x, 
            _parent.transform.position.y - _parent.GetComponent<CircleCollider2D>().radius);
    }

    private void SpawnStuckKnife(KnifeGetsIntoTargetEvent eventData)
    {
        Instantiate(_knifeStuckPrefab, _spawnPoint, Quaternion.identity, _parent);
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
