using SimpleEventBus.Events;

namespace Events
{
    public class RandomizerGeneratedValuesEvent : EventBase
    {
        public int WoodDurability { get; }
        public int KnifeCountUI { get; }
        
        public RandomizerGeneratedValuesEvent(int woodDurability, int knifeCountUI)
        {
            WoodDurability = woodDurability;
            KnifeCountUI = knifeCountUI;
        }
    }
}