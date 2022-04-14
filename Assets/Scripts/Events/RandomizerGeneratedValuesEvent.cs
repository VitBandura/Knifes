using SimpleEventBus.Events;

namespace Events
{
    public class RandomizerGeneratedValuesEvent : EventBase
    {
        public int WoodDurability { get; }
        public int KnifeCountUI { get; }
        
        public int StuckKnivesCount { get; }
        
        public int CircularSpawningPointsCount { get; }

        public RandomizerGeneratedValuesEvent(
            int woodDurability, int knifeCountUI, int stuckKnivesCount, int circularSpawningPointsCount)
        {
            WoodDurability = woodDurability;
            KnifeCountUI = knifeCountUI;
            StuckKnivesCount = stuckKnivesCount;
            CircularSpawningPointsCount = circularSpawningPointsCount;
        }
    }
}