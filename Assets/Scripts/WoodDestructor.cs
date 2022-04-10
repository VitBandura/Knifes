using UnityEngine;

public class WoodDestructor : MonoBehaviour
{
    private const float MAX_DESTROYED_PART_SPEED = 300;
    private const float MIN_DESTROYED_PART_SPEED = 150;
    private const float MAX_DESTROYED_PART_TORQUE = 200;
    private const float MIN_DESTROYED_PART_TORQUE = -200;
    
    private Rigidbody2D[] _partsOfTarget;

    private void Awake()
    {
        GetAllPartsOfDestroyedTarget();
    }

    private void OnEnable()
    {
        ExplodeTargetParts();
    }

    private void GetAllPartsOfDestroyedTarget()
    {
        _partsOfTarget = GetComponentsInChildren<Rigidbody2D>();
    }

   private void ExplodeTargetParts()
    {
        GetAllPartsOfDestroyedTarget();
        foreach (var part in _partsOfTarget)
        {
            var destroyedPartSpeed = Random.Range(MIN_DESTROYED_PART_SPEED, MAX_DESTROYED_PART_SPEED);
            var destroyedPartTorque = Random.Range(MIN_DESTROYED_PART_TORQUE, MAX_DESTROYED_PART_TORQUE);
            var forceUnit = Vector2.up * destroyedPartSpeed;
            part.AddForce(forceUnit);
            part.AddTorque(destroyedPartTorque);
        }
    }
}
