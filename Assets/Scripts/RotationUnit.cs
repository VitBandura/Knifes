using System;
using UnityEngine;

[Serializable]
public class RotationUnit
{
    public float Speed => _speed;
    public float Duration => _duration;
    
    [SerializeField] private float _speed;
    [SerializeField] private float _duration;
}
