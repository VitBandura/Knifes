using SimpleEventBus.Events;

namespace Events
{
    public class RandomizerGeneratedValuesEvent : EventBase
    {
        public int WoodDurability { get; }
        public int KnifeCountUI { get; }
        
        public int KnivesInWoodCount { get; }
        
        public int CircularSpawningPointsCount { get; }

        public RandomizerGeneratedValuesEvent(
            int woodDurability, int knifeCountUI, int knivesInWoodCount, int circularSpawningPointsCount)
        {
            WoodDurability = woodDurability;
            KnifeCountUI = knifeCountUI;
            KnivesInWoodCount = knivesInWoodCount;
            CircularSpawningPointsCount = circularSpawningPointsCount;
        }
    }
}