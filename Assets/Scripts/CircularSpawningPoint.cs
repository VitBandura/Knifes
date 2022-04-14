using UnityEngine;

public class CircularSpawningPoint
{
    public Vector3 Position => _position;
    public Quaternion Rotation => _rotation;
    
    private readonly Vector3 _position;
    private readonly Quaternion _rotation;
    
    public CircularSpawningPoint(Vector3 position, Quaternion rotation)
    {
        _position = position;
        _rotation = rotation;
    }
}
