using SimpleEventBus.Events;

namespace Events
{
    public class RandomizerGeneratedValuesEvent : EventBase
    {
        public float WoodDurability { get; }
        public float KnifeCountUI { get; }
        
        public RandomizerGeneratedValuesEvent(float woodDurability, float knifeCountUI)
        {
            WoodDurability = woodDurability;
            KnifeCountUI = knifeCountUI;
        }
    }
}