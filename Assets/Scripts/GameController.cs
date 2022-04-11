using Events;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    private const int MAX_LEVEL_GENERAL_VALUE = 11;
    private const int MIN_LEVEL_GENERAL_VALUE = 5;
    
    private int _randomLevelGeneralValue;
    private int _woodDurability;
    private int _knifeCountUI;

    private void Start()
    {
        GenerateLevelGeneralValue();
    }

    private void GenerateLevelGeneralValue()
    {
        _randomLevelGeneralValue = Random.Range(MIN_LEVEL_GENERAL_VALUE, MAX_LEVEL_GENERAL_VALUE);
        _woodDurability = _randomLevelGeneralValue;
        _knifeCountUI = _randomLevelGeneralValue;

        EventStreams.GameEvents.Publish(new GameStartedEvent(_woodDurability, _knifeCountUI));
    }
}
