using AppLogic.Events;
using Microsoft.Extensions.Logging;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic.Events
{
    internal class UserRegisteredEventHandler : IIntegrationEventHandler<UserRegisteredIntegrationEvent>
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<UserRegisteredEventHandler> _logger;

        public UserRegisteredEventHandler(ICustomerRepository customerRepository, ILogger<UserRegisteredEventHandler> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public Task Handle(UserRegisteredIntegrationEvent @event)
        {
            _logger.LogDebug($"OrderManagement - Handeling adding an new customer. Id:{@event.Id} ");

            return Task.Run(async () =>
            {

                Customer customer = await _customerRepository.GetByStringIdAsync(@event.CustomerId);
                if (customer != null)
                {
                    _logger.LogDebug($"OrderManagement - No Customer added. a customer with id:{@event.CustomerId} already exists. Id:{@event.Id}");
                    return;
                }

                customer = Customer.CreateNew(@event.CustomerId, @event.Name);
                await _customerRepository.AddAsync(customer);
                _logger.LogDebug($"OrderManagement - Customer with name: '{@event.Name}' has been added. Id:{@event.Id}");


            });
        }
    }
}
