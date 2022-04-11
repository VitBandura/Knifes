using System.Collections.Generic;
using UnityEngine;

public class CircularSpawner : MonoBehaviour
{
   private const int FULL_DEGREE_ANGLE = 360;
   
   [SerializeField] private GameObject _coinPrefab;
   [SerializeField] private int _positionsCount;
   [SerializeField] private float _radius;

   private float _angularStepInDeg;
   private float _angularStepInRad;
   private List<CircularSpawningPoint> _spawningPoints = new();
   

   private void Awake()
   {
      _angularStepInDeg = FULL_DEGREE_ANGLE / _positionsCount;
      _angularStepInRad = _angularStepInDeg * Mathf.Deg2Rad;

      for (var i = 0; i < _positionsCount; i++)
      {
         var currentAngle = _angularStepInRad * i;
         
         var xPosition = _radius * Mathf.Cos(currentAngle);
         var yPosition = _radius * Mathf.Sin(currentAngle);
         var position = new Vector3(xPosition, yPosition);
         Debug.Log(position);
         
         var rotation = Quaternion.identity;
         
         _spawningPoints.Add(new CircularSpawningPoint(position, rotation));
      }
   }

   private void Update()
   {
      if (Input.GetMouseButtonDown(0))
      {
         foreach (var spawnPoint in _spawningPoints)
         {
            Instantiate(_coinPrefab, spawnPoint.Position, spawnPoint.Rotation);
         }
      }
   }
}
