using Events;
using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButtonUI : MonoBehaviour
{
    private Button _button;
    private CompositeDisposable _subscriptions;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.gameObject.SetActive(false);
       
      
        InitializeSubscriptions();
    }

    private void InitializeSubscriptions()
    {
        _subscriptions = new CompositeDisposable
        {
            EventStreams.GameEvents.Subscribe<TargetDestroyedEvent>(ShowButton),
            EventStreams.GameEvents.Subscribe<GameOverEvent>(ShowButton)
        };
    }

    //TODO rework show button methods
    private void ShowButton(GameOverEvent eventData)
    {
        _button.gameObject.SetActive(true);
    }

    private void ShowButton(TargetDestroyedEvent eventData)
    {
       _button.gameObject.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDestroy()
    {
        _subscriptions.Dispose();
    }
}
