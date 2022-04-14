using Events;
using UnityEngine;
using Random = UnityEngine.Random;

public class GeneralRandomizer : MonoBehaviour
{
    private const int MAX_LEVEL_GOAL = 11;
    private const int MIN_LEVEL_GOAL = 5;
    
    private const int MAX_STUCK_KNIVES_COUNT = 4;
    private const int MIN_STUCK_KNIVES_COUNT = 1;
    
    private const int MAX_CIRCULAR_SPAWNING_POINTS_COUNT = 9;
    private const int MIN_CIRCULAR_SPAWNING_POINTS_COUNT = 3;
    
    private int _levelGoal;
    private int _woodDurability;
    private int _knifeCountUI;
    private int _stuckKnivesCount;
    private int _circularSpawningPointsCount;
        
    private void Start()
    {
        GenerateLevelGoal();
        GenerateStuckKnifeCount();
        GenerateCircularSpawningPointsCount();
        
        EventStreams.GameEvents.Publish(new RandomizerGeneratedValuesEvent(
            _woodDurability, _knifeCountUI, _stuckKnivesCount, _circularSpawningPointsCount));
    }

    private void GenerateLevelGoal()
    {
        _levelGoal = Random.Range(MIN_LEVEL_GOAL, MAX_LEVEL_GOAL);
        _woodDurability = _levelGoal;
        _knifeCountUI = _levelGoal;
    }
    
    private void GenerateStuckKnifeCount()
    {
        _stuckKnivesCount = Random.Range(MAX_STUCK_KNIVES_COUNT, MIN_STUCK_KNIVES_COUNT);
    }

    private void GenerateCircularSpawningPointsCount()
    {
        _circularSpawningPointsCount =
            Random.Range(MIN_CIRCULAR_SPAWNING_POINTS_COUNT, MAX_CIRCULAR_SPAWNING_POINTS_COUNT);
    } 
    
}
