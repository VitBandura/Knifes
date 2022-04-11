using UnityEngine;

[CreateAssetMenu (fileName = "CoinChanceSettings", menuName = "CoinChanceSettings")]
  public class CoinChanceSettings : ScriptableObject
  {
      public float Chance => _chance;
      
      [SerializeField] private float _chance;
  }
