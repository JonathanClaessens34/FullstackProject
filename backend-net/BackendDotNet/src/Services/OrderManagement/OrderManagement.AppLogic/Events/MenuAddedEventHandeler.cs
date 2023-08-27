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
    internal class MenuAddedEventHandeler : IIntegrationEventHandler<MenuAddedIntegrationEvent>
    {
        private readonly ICocktailMenuRepository _cocktaimMenuRepository;
        private readonly ILogger<MenuAddedEventHandeler> _logger;

        public MenuAddedEventHandeler(ICocktailMenuRepository cocktaimMenuRepository, ILogger<MenuAddedEventHandeler> logger)
        {
            _cocktaimMenuRepository = cocktaimMenuRepository;
            _logger = logger;
        }

        public Task Handle(MenuAddedIntegrationEvent @event)
        {
            _logger.LogDebug($"OrderManagement - handeling adding an Menu. Id:{@event.Id}");

            return Task.Run(async () =>
            {

                CocktailMenu menu = await _cocktaimMenuRepository.GetByStringIdAsync(@event.menuId);
                if (menu == null)
                {
                    menu = CocktailMenu.CreateNew(@event.menuId, @event.BarName);
                    await _cocktaimMenuRepository.addAsync(menu);
                    return;
                }

                if(menu.BarName != @event.BarName && @event.BarName != string.Empty)
                {
                    menu.BarName = @event.BarName;
                }

                await _cocktaimMenuRepository.saveChangesAsync();

                

            });



        }
    }
}
