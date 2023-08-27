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
    internal class CocktailDeletedFromMenuEventHandeler : IIntegrationEventHandler<CocktailDeletedFromMenuIntegrationEvent>
    {

        private readonly ICocktailMenuRepository _menuRepository;
        private readonly ILogger<CocktailDeletedFromMenuEventHandeler> _logger;
        private readonly IMenuItemRepository _menuItemRepository;

        public CocktailDeletedFromMenuEventHandeler(ICocktailMenuRepository cocktailMenuRepository, ILogger<CocktailDeletedFromMenuEventHandeler> logger, IMenuItemRepository menuItemRepository)
        {
            _menuRepository = cocktailMenuRepository;
            _logger = logger;
            _menuItemRepository = menuItemRepository;
        }

        public Task Handle(CocktailDeletedFromMenuIntegrationEvent @event)
        {
            _logger.LogDebug($"OrderManagement - Handeling adding an new cocktail. Id:{@event.Id} ");

            return Task.Run(async () =>
            {

                CocktailMenu cocktailMenu = await _menuRepository.GetByStringIdAsync(@event.MenuId);
                cocktailMenu.DeleteCocktail(@event.SerialNumber);
                _menuRepository.saveChangesAsync();


                _logger.LogDebug($"OrderManagement - CocktailMenu with serialnr: '{@event.MenuId}' has been deletd. Id:{@event.Id}");


            });
        }


    }
}
