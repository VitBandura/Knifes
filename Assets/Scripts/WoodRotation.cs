using UnityEngine;

public class WoodRotation : MonoBehaviour
{
    private WheelJoint2D _wheelJoint2D;
    private JointMotor2D _jointMotor2D;

    [SerializeField] private float _speed;

    private void Awake()
    {
        _wheelJoint2D = GetComponent<WheelJoint2D>();
        _jointMotor2D = _wheelJoint2D.motor;
        _jointMotor2D.motorSpeed = _speed;
        _wheelJoint2D.motor = _jointMotor2D;
    }
}
