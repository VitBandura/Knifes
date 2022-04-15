using UnityEngine;

    public class AngularUnit
    {
        public float Cos => _cos;
        public float Sin => _sin;
        public Quaternion Rotation => _rotation;
    
        private readonly float _cos;
        private readonly float _sin;
        private readonly Quaternion _rotation;
    
        public AngularUnit(float cos, float sin, Quaternion rotation)
        {
            _cos = cos;
            _sin = sin;
            _rotation = rotation;
        }
    }
