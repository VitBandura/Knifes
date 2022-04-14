using System.Collections.Generic;
using Events;
using SimpleEventBus.Disposables;
using UnityEngine;

public class KnifePool : MonoBehaviour
{
   [SerializeField] private GameObject _knifePrefab;
   [SerializeField] private float _poolSize;

   private Queue<GameObject> _knifePool;
   private CompositeDisposable _subscriptions; 
      
   private void Awake()
   {
      InitializeSubscriptions();
      InitializePool();
   }

   private void InitializeSubscriptions()
   {
      _subscriptions = new CompositeDisposable
      {
         EventStreams.GameEvents.Subscribe<KnifeGetsIntoTargetEvent>(ReleaseKnife)
      };
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
         var knife = Instantiate(_knifePrefab);
         knife.SetActive(false);
         _knifePool.Enqueue(knife);
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
   
   private void ReleaseKnife(KnifeGetsIntoTargetEvent eventData)
   {
      eventData.Knife.SetActive(false);
      _knifePool.Enqueue(eventData.Knife);
   }

   private void OnDestroy()
   {
      _subscriptions.Dispose();
   }
}
