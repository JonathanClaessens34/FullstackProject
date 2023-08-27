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
    internal class CocktailDeletedEventHandler : IIntegrationEventHandler<CocktailDeletedIntegrationEvent>
    {

        private readonly ICocktailRepository _cocktailRepository;
        private readonly ILogger<CocktailDeletedEventHandler> _logger;

        public CocktailDeletedEventHandler(ICocktailRepository cocktailRepository, ILogger<CocktailDeletedEventHandler> logger)
        {
            _cocktailRepository = cocktailRepository;
            _logger = logger;
        }

        public Task Handle(CocktailDeletedIntegrationEvent @event)
        {
            _logger.LogDebug($"OrderManagement - Handeling adding an new cocktail. Id:{@event.Id} ");

            return Task.Run(async () =>
            {

                Cocktail cocktail = await _cocktailRepository.GetBySerialNumberAsync(@event.SerialNumber);

                await _cocktailRepository.DeleteAsync(cocktail);

                _logger.LogDebug($"OrderManagement - Cocktail with serialnr: '{@event.SerialNumber}' has been deletd. Id:{@event.Id}");


            });
        }



    }
}
