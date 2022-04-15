using UnityEngine;

    public class AngularUnit
    {
        public float Cos => _cos;
        public float Sin => _sin;
        public float Angle => _angle;
    
        private readonly float _cos;
        private readonly float _sin;
        private readonly float _angle;
    
        public AngularUnit(float cos, float sin, float angle)
        {
            _cos = cos;
            _sin = sin;
            _angle = angle;
        }
    }
