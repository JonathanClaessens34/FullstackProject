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
    internal class CocktailAddedToMenuEventHandler : IIntegrationEventHandler<CocktailAddedToMenuIntegrationEvent>
    {

        private readonly ICocktailMenuRepository _cocktailMenuRepository;
        private readonly ICocktailRepository _cocktailRepository;
        private readonly ILogger<CocktailAddedToMenuEventHandler> _logger;
        private readonly IMenuItemRepository _menuItemRepository;


        public CocktailAddedToMenuEventHandler(ICocktailMenuRepository cocktailMenuRepository, ICocktailRepository cocktailRepository, ILogger<CocktailAddedToMenuEventHandler> logger, IMenuItemRepository menuItemRepository)
        {
            _cocktailMenuRepository = cocktailMenuRepository;
            _cocktailRepository = cocktailRepository;
            _logger = logger;
            _menuItemRepository = menuItemRepository;
        }

        public Task Handle(CocktailAddedToMenuIntegrationEvent @event)
        {
            _logger.LogDebug($"OrderManagement - handeling adding a cocktail to menu. Id:{@event.Id}");
            return Task.Run(async () =>
            {

                double newPrice = Convert.ToDouble( @event.Price );
                Cocktail cocktail = await _cocktailRepository.GetBySerialNumberAsync(@event.SerialNumber);
                CocktailMenu menu = await _cocktailMenuRepository.GetByStringIdAsync(@event.menuId);

                if (cocktail == null || menu == null)
                {
                    _logger.LogDebug($"OrderManagement cocktail or menu given does not exist. Id:{@event.Id}");
                    return;
                }

                MenuItem newMenuItem = new MenuItem(cocktail, newPrice);
                await _menuItemRepository.addAsync(newMenuItem);
                menu.AddCocktail(newMenuItem);
                await _cocktailMenuRepository.saveChangesAsync();
                //await _cocktailMenuRepository.saveChangesAsyncAlways(menu);
                _logger.LogDebug($"OrderManagement - Cocktail with id:{@event.SerialNumber} has been added to menu with id:{@event.menuId}. Id:{@event.Id}");

            });
        }
    }
}
