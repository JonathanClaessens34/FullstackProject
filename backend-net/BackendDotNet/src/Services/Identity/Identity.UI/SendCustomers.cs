using AppLogic.Events;
using Identity.UI.Events;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace Identity.UI
{
    public class SendCustomers
    {

        public static void sendCustomers(IEventBus eventBus)
        {
            var @event = new UserRegisteredIntegrationEvent
            {
                CustomerId = "1f974d9f-41d3-4b86-b8d5-058859808534",
                Name = "alice"
            };
            eventBus.Publish(@event);

            @event = new UserRegisteredIntegrationEvent
            {
                CustomerId = "dba5b341-aadb-4fba-9824-8850db4bc5b5",
                Name = "bob"
            };
            eventBus.Publish(@event);

        }

    }
}
