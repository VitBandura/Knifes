using Events;
using SimpleEventBus.Disposables;
using UnityEngine;

public class WoodManager : MonoBehaviour
{
    [SerializeField] private GameObject _woodPrefab;
    [SerializeField] private GameObject _woodDestroyedPrefab;

    private GameObject _wood;
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
        
        _wood = Instantiate(_woodPrefab);
        _wood.transform.position = _position;
        _wood.SetActive(false);
        
        _woodDestroyed = Instantiate(_woodDestroyedPrefab);
        _woodDestroyed.transform.position = _position;
        _woodDestroyed.SetActive(false);
        
        _wood.SetActive(true);
    }

    private void HandleWoodDestruction(TargetDestroyedEvent obj)
    {
        var stuckKnives = _wood.GetComponentsInChildren<Transform>();
        for (var i = 2; i < stuckKnives.Length; i++)
        {
            stuckKnives[i].parent = _woodDestroyed.transform;
            stuckKnives[i].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        _wood.SetActive(false);
        _woodDestroyed.transform.rotation = _wood.transform.rotation;
        _woodDestroyed.SetActive(true);
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
