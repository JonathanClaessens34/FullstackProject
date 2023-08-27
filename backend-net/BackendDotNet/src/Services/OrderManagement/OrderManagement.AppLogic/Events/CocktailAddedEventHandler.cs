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
    internal class CocktailAddedEventHandler : IIntegrationEventHandler<CocktailAddedIntegrationEvent>
    {

        private readonly ICocktailRepository _cocktailRepository;
        private readonly ILogger<CocktailAddedEventHandler> _logger;

        public CocktailAddedEventHandler(ICocktailRepository cocktailRepository, ILogger<CocktailAddedEventHandler> logger)
        {
            _cocktailRepository = cocktailRepository;
            _logger = logger;
        }

        public Task Handle(CocktailAddedIntegrationEvent @event)
        {
            _logger.LogDebug($"OrderManagement - Handeling adding an new cocktail. Id:{@event.Id} ");

            return Task.Run(async () =>
            {

                Cocktail cocktail = await _cocktailRepository.GetBySerialNumberAsync(@event.SerialNumber);
                
                if (cocktail == null)
                {
                    cocktail = Cocktail.CreateNew(@event.SerialNumber, @event.Name, @event.ImageUrl);
                    await _cocktailRepository.AddAsync(cocktail);
                    return;
                }

                //serie nummer kan niet aangepast worden dus dit moet nie
                //if(@event.SerialNumber != string.Empty || @event.SerialNumber != cocktail.SerialNumber.ToString()) {
                //    cocktail.SerialNumber = @event.SerialNumber;
                //}

                if (@event.Name != string.Empty || @event.Name != cocktail.Name)
                {
                    cocktail.Name = @event.Name;
                }

                if (@event.ImageUrl != string.Empty || @event.ImageUrl != cocktail.ImageUrl)
                {
                    cocktail.ImageUrl = @event.ImageUrl;
                }

                await _cocktailRepository.CommitTrackedChangesAsync();

                _logger.LogDebug($"OrderManagement - Cocktail with serialnr: '{@event.SerialNumber}' has been added. Id:{@event.Id}");
                

            });
        }
    }
}
