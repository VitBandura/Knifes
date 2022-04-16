using Events;
using UnityEngine;
using Random = UnityEngine.Random;

public class GeneralRandomizer : MonoBehaviour
{
    private const float MAX_LEVEL_GOAL = 8f;
    private const float MIN_LEVEL_GOAL = 4f;
    
    private float _levelGoal;
    private float _woodDurability;
    private float _knifeCountUI;
   
    private void Start()
    {
        GenerateLevelGoal();
        
        EventStreams.GameEvents.Publish(new RandomizerGeneratedValuesEvent(_woodDurability, _knifeCountUI));
    }

    private void GenerateLevelGoal()
    {
        _levelGoal = Random.Range(MIN_LEVEL_GOAL, MAX_LEVEL_GOAL);
        _woodDurability = _levelGoal;
        _knifeCountUI = _levelGoal;
    }
}
