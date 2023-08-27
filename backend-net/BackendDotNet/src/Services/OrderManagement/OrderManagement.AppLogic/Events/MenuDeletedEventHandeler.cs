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
    internal class MenuDeletedEventHandeler : IIntegrationEventHandler<MenuDeletedIntegrationEvent>
    {

        private readonly ICocktailMenuRepository _menuRepository;
        private readonly ILogger<MenuDeletedEventHandeler> _logger;

        public MenuDeletedEventHandeler(ICocktailMenuRepository cocktailMenuRepository, ILogger<MenuDeletedEventHandeler> logger)
        {
            _menuRepository = cocktailMenuRepository;
            _logger = logger;
        }

        public Task Handle(MenuDeletedIntegrationEvent @event)
        {
            _logger.LogDebug($"OrderManagement - Handeling adding an new cocktail. Id:{@event.Id} ");

            return Task.Run(async () =>
            {

                CocktailMenu cocktailMenu = await _menuRepository.GetByStringIdAsync(@event.MenuId);

                await _menuRepository.DeleteAsync(cocktailMenu);

                _logger.LogDebug($"OrderManagement - CocktailMenu with serialnr: '{@event.MenuId}' has been deletd. Id:{@event.Id}");


            });
        }



    }
}
