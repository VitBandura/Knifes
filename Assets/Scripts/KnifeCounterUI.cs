using Events;
using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.UI;

public class KnifeCounterUI : MonoBehaviour
{
    [SerializeField] private GameObject _knifeIcon;
    [SerializeField] private Color _usedKnifeIconColor;

    private CompositeDisposable _subscriptions;
    private int _knifeCount;
    private int _knifeIconIndex;
    
    private void Awake()
    {
        InitializeSubscriptions();
    }

    private void InitializeSubscriptions()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.GameEvents.Subscribe<RandomizerGeneratedValuesEvent>(InitializeKnifeCounterBar),
            EventStreams.GameEvents.Subscribe<KnifeWasThrownEvent>(ReduceKnifeCount)
        };
    }

    private void InitializeKnifeCounterBar(RandomizerGeneratedValuesEvent eventData)
    {
        _knifeCount = eventData.KnifeCountUI;
        
        for (var i = 0; i < _knifeCount; i++)
        {
            Instantiate(_knifeIcon, transform);
        }
    }

    private void ReduceKnifeCount(KnifeWasThrownEvent eventData)
    {
        transform.GetChild(_knifeIconIndex).GetComponent<Image>().color = _usedKnifeIconColor;
        _knifeIconIndex++;
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
