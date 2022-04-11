using UnityEngine;

public class CircularSpawningPoint
{
    private readonly Vector3 _position;
    private readonly Quaternion _rotation;
    
    public Vector3 Position => _position;
    public Quaternion Rotation => _rotation;

    public CircularSpawningPoint(Vector3 position, Quaternion rotation)
    {
        _position = position;
        _rotation = rotation;
    }
}
