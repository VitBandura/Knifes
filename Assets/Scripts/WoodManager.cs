using Events;
using SimpleEventBus.Disposables;
using UnityEngine;

public class WoodManager : MonoBehaviour
{
    [SerializeField] private GameObject _woodRotator;
    [SerializeField] private GameObject _woodDestroyedPrefab;
    
    private GameObject _woodDestroyed;
    private Vector3 _position;

    private CompositeDisposable _subscriptions;
    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.GameEvents.Subscribe<TargetDestroyedEvent>(HandleWoodDestruction)
        };

        _position = transform.position;
        
        _woodDestroyed = Instantiate(_woodDestroyedPrefab);
        _woodDestroyed.transform.position = _position;
        _woodDestroyed.SetActive(false);
        
        _woodRotator.SetActive(true);
    }

    private void HandleWoodDestruction(TargetDestroyedEvent obj)
    {
        var stuckKnives = _woodRotator.GetComponentsInChildren<Transform>();
        for (var i = 2; i < stuckKnives.Length; i++)
        {
            stuckKnives[i].parent = _woodDestroyed.transform;
            stuckKnives[i].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        _woodRotator.SetActive(false);
        _woodDestroyed.transform.rotation = _woodRotator.transform.rotation;
        _woodDestroyed.SetActive(true);
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
