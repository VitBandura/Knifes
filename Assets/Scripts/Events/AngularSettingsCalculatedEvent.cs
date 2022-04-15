using System.Collections.Generic;
using SimpleEventBus.Events;

namespace Events
{
    public class AngularSettingsCalculatedEvent : EventBase
    {
        public List<AngularUnit> AngularSettings => _angularSettings;
        
        private List<AngularUnit> _angularSettings;

        public AngularSettingsCalculatedEvent(List<AngularUnit> angularSettings)
        {
            _angularSettings = angularSettings;
        }
    }
}