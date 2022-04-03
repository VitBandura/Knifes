using System.Collections.Generic;
using UnityEngine;

public class KnifePool : MonoBehaviour
{
   [SerializeField] private GameObject _knifePrefab;
   [SerializeField] private float _poolSize;

   private Queue<GameObject> _knifePool;
   private void Awake()
   {
      InitializePool();
   }
   
   private void InitializePool()
   {
      _knifePool = new Queue<GameObject>();
      FillPoolWithNewKnives();
   }

   private void FillPoolWithNewKnives()
   {
      for (var i = 0; i < _poolSize; i++)
      {
         var prefab = Instantiate(_knifePrefab);
         prefab.SetActive(false);
         _knifePool.Enqueue(prefab);
      }
   }

   private bool IsPoolEmpty()
   {
      return _knifePool.Count < 1;
   }

   public GameObject TakeKnifeFromPool()
   {
      if (IsPoolEmpty())
      {
         FillPoolWithNewKnives();
      }
      return _knifePool.Dequeue();
   }
    /*
   private void ReturnBombIntoPool(BombDetonationEvent eventData)
   {
      eventData.Bomb.SetActive(false);
      _bombPool.Enqueue(eventData.Bomb);
   }
   */
}
