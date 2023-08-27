using AppLogic.Events;

namespace Identity.UI.Events
{
    public class UserRegisteredIntegrationEvent : IntegrationEvent
    {
        public string CustomerId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
