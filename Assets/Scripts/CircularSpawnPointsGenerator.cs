using System.Collections.Generic;
using Events;
using SimpleEventBus.Disposables;
using UnityEngine;

public class CircularSpawnPointsGenerator : MonoBehaviour
{
   private const int FULL_DEGREE_ANGLE = 360;
   
   [SerializeField] private GameObject _coinPrefab;
   [SerializeField] private GameObject _knifeStuckPrefab;
   [SerializeField] private Transform _parent;

   private CompositeDisposable _subscriptions;
   private int _positionsCount;
   private float _radius;
   private float _angularStepInDeg;
   private float _angularStepInRad;
   private List<CircularSpawningPoint> _spawningPoints = new();
   
   private void Awake()
   {
      InitializeSubscriptions();
   }

   private void CalculatePositions()
   {
      _radius = _parent.GetComponent<CircleCollider2D>().radius +
                _coinPrefab.GetComponent<CircleCollider2D>().radius;

      _angularStepInDeg = FULL_DEGREE_ANGLE / _positionsCount;
      _angularStepInRad = _angularStepInDeg * Mathf.Deg2Rad;

      for (var i = 0; i < _positionsCount; i++)
      {
         var currentAngleInRad = _angularStepInRad * i;
         var currentAngleInDeg = _angularStepInDeg * i;

         var xPosition = _radius * Mathf.Cos(currentAngleInRad);
         var yPosition = _radius * Mathf.Sin(currentAngleInRad);
         var position = new Vector3(xPosition + _parent.position.x, yPosition + _parent.position.y);

         var rotation = Quaternion.Euler(0, 0, currentAngleInDeg - 90);

         _spawningPoints.Add(new CircularSpawningPoint(position, rotation));
      }
   }
  
  private void InitializeSubscriptions()
  {
     _subscriptions = new CompositeDisposable
     {
        EventStreams.GameEvents.Subscribe<RandomizerGeneratedValuesEvent>(GetPositionsCount)
     };
  }
  
  //todo refactor this
  private void GetPositionsCount(RandomizerGeneratedValuesEvent eventData)
  {
     _positionsCount = eventData.CircularSpawningPointsCount;
     
     CalculatePositions();
     
     foreach (var spawnPoint in _spawningPoints)
     {
        Instantiate(_knifeStuckPrefab, spawnPoint.Position, spawnPoint.Rotation, _parent);
     }
  }

  private void OnDestroy()
  {
     _subscriptions.Dispose();
  }
}
