using UnityEngine;

public class WoodDestruction : MonoBehaviour
{
    private Rigidbody2D[] _partsOfTarget;
    private Vector3[] _startPositionsOfParts;

    private void Awake()
    {
        GetAllPartsOfDestroyedTarget();
        SavePartsStartPositions();
    }

    private void GetAllPartsOfDestroyedTarget()
    {
        _partsOfTarget = GetComponentsInChildren<Rigidbody2D>();
    }

    private void SavePartsStartPositions()
    {
        _startPositionsOfParts = new Vector3[_partsOfTarget.Length];
        
        for (var i = 0; i < _partsOfTarget.Length; i++)
        {
            _startPositionsOfParts[i] = _partsOfTarget[i].transform.position;
        }
    }
    
    private void RefreshPartsPositions()
    {
        for (var i = 0; i < _partsOfTarget.Length; i++)
        {
            _partsOfTarget[i].transform.position = _startPositionsOfParts[i];
        }
    }

    //TODO introduce variables and add random
    private void ExplodeTargetParts()
    {
        foreach (var part in _partsOfTarget)
        {
            part.AddForce(Vector2.up * 500);
            part.AddTorque(500);
        }
    }
}
