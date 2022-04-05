using Events;
using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.UI;

public class KnifeCounterUI : MonoBehaviour
{
    [SerializeField] private GameObject _knifeIcon;
    [SerializeField] private float _knifeCount;
    [SerializeField] private Color _usedKnifeIconColor;

    private CompositeDisposable _subscriptions;
    private int _knifeIconIndex;
    private void Awake()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.GameEvents.Subscribe<KnifeWasThrownEvent>(ReduceKnifeCount)
        };
        
        for (var i = 0; i < _knifeCount; i++)
        {
            Instantiate(_knifeIcon, transform);
        }
    }

    private void ReduceKnifeCount(KnifeWasThrownEvent obj)
    {
        transform.GetChild(_knifeIconIndex).GetComponent<Image>().color = _usedKnifeIconColor;
        _knifeIconIndex++;
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
