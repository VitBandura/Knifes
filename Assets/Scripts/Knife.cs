using Events;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private const float FULL_DEGREE_ANGLE = 360f;

    [SerializeField] private float _tossAwaySpeed;
    [SerializeField] private float _tossAwayRotation;
    
    private Rigidbody2D _rigidbody2D;
    private bool _isTossedAway;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

   private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.CompareTag("StuckKnife") && !_isTossedAway)
      {
          var tossingVelocity = Vector2.down * _tossAwaySpeed;
          var tossingRotation = FULL_DEGREE_ANGLE * _tossAwayRotation;
          
          _rigidbody2D.velocity = tossingVelocity;
          _rigidbody2D.angularVelocity = tossingRotation;
          _isTossedAway = true;
          
          Debug.Log("loss");
          EventStreams.GameEvents.Publish(new GameOverEvent());
      }
    }

   private void OnTriggerStay2D(Collider2D other)
   {
       if (other.GetComponent<Wood>() != null && !_isTossedAway)
       {
           EventStreams.GameEvents.Publish(new KnifeGetsIntoTargetEvent(gameObject));
       }
   }
}
