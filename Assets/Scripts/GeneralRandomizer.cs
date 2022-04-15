using Events;
using UnityEngine;
using Random = UnityEngine.Random;

public class GeneralRandomizer : MonoBehaviour
{
    private const int MAX_LEVEL_GOAL = 11;
    private const int MIN_LEVEL_GOAL = 5;
    
    private int _levelGoal;
    private int _woodDurability;
    private int _knifeCountUI;
   
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
