using System.Collections;
using UnityEngine;

public class WoodRotator : MonoBehaviour
{
    private WheelJoint2D _wheelJoint2D;
    private JointMotor2D _jointMotor2D;

    [SerializeField] private RotationUnit[] _rotationModes;

    private void Awake()
    {
        InitializeWheelJointComponent();
        StartCoroutine(RotateInMixedMode());
    }

    private void InitializeWheelJointComponent()
    {
        _wheelJoint2D = GetComponent<WheelJoint2D>();
        _jointMotor2D = _wheelJoint2D.motor;
    }

    private IEnumerator RotateInMixedMode()
    {
        var unitIndex = 0;
        var unitIndexMax = _rotationModes.Length - 1;
        while (true)
        {
            _jointMotor2D.motorSpeed = _rotationModes[unitIndex].Speed;
            _wheelJoint2D.motor = _jointMotor2D;

            yield return new WaitForSeconds(_rotationModes[unitIndex].Duration);

            if (unitIndex < unitIndexMax)
            {
                unitIndex++;
            }
            else
            {
                unitIndex = 0;
            }
        }
    }
}
