using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private GameObject _targetPrefab;
    [SerializeField] private GameObject _targetDestroyedPrefab;

    private GameObject _target;
    private GameObject _targetDestroyed;
    private Vector3 _mainPosition;
    private void Awake()
    {
        _mainPosition = transform.position;
        InitializeTarget();
        InitializeTargetDestroyed();
        
        ShowTarget();
    }

    private void InitializeTarget()
    {
        _target = Instantiate(_targetPrefab);
        _target.SetActive(false);
        _target.transform.position = _mainPosition;
    }
    
    private void InitializeTargetDestroyed()
    {
        _targetDestroyed = Instantiate(_targetDestroyedPrefab);
        _targetDestroyed.SetActive(false);
        _targetDestroyed.transform.position = _mainPosition;
    }

    private void ShowTarget()
    {
        _targetDestroyed.SetActive(false);
        _target.SetActive(true);
    }

    private void ShowDestroyedTarget()
    {
        _target.SetActive(false);
        _targetDestroyed.SetActive(true);
    }
}
