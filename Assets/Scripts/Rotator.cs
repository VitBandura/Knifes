using System;
using System.Collections;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _accelerationTime;
    [SerializeField] private float _speed;
    [SerializeField] private float _duration;
    [SerializeField] private float _slowingTime;

    private void Start()
    {
        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        var speed = 0f;
        while (speed < _speed)
        {
            transform.Rotate(Vector3.forward, speed * Time.deltaTime);
            if (_accelerationTime == 0)
            {
                speed = _speed;
                break;
            }
            speed += _speed * Time.deltaTime / _accelerationTime;
            yield return null;
        }
        while (_duration > 0)
        {
            transform.Rotate(Vector3.forward, speed * Time.deltaTime);
            _duration -= Time.deltaTime;
            yield return null;
        }

        while (speed > 0)
        {
            if (_slowingTime == 0)
            {
                speed = 0;
                yield return null;
            }
            transform.Rotate(Vector3.forward, speed * Time.deltaTime);
            speed -= _speed * Time.deltaTime / _slowingTime;
            yield return null;
        }
        
        
    }
}
