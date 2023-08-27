using Microsoft.Extensions.Logging;
using Moq;
using OrderManagement.AppLogic.Events;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic.Tests.Events
{
    public class CocktailAddedToMenuEventHandlerTests : TestBase
    {
        private readonly Mock<ICocktailMenuRepository> _mockCocktailMenuRepository;
        private readonly Mock<ICocktailRepository> _mockCocktailRepository;
        private readonly Mock<ILogger<CocktailAddedToMenuEventHandler>> _mockLogger;
        private readonly Mock<IMenuItemRepository> _mockMenuItemRepository;
        private readonly CocktailAddedToMenuEventHandler _eventHandler;
        private readonly CocktailAddedToMenuIntegrationEvent _event;
        private readonly Cocktail _cocktail;
        private readonly CocktailMenu _cocktailMenu;

        public CocktailAddedToMenuEventHandlerTests()
        {
            _mockCocktailMenuRepository = new Mock<ICocktailMenuRepository>();
            _mockCocktailRepository = new Mock<ICocktailRepository>();
            _mockLogger = new Mock<ILogger<CocktailAddedToMenuEventHandler>>();
            _mockMenuItemRepository = new Mock<IMenuItemRepository>();
            _eventHandler = new CocktailAddedToMenuEventHandler(_mockCocktailMenuRepository.Object, _mockCocktailRepository.Object, _mockLogger.Object, _mockMenuItemRepository.Object);
            _event = new CocktailAddedToMenuIntegrationEvent { SerialNumber = "123", menuId = "1", Price = "5.5" };
            _cocktail = new Cocktail ("1234567891011", "Mojito", "www.mojito.com/image.jpg" );
            _cocktailMenu = new CocktailMenu("bar1");
        }

        [Test]
        public async Task Handle_CocktailAndMenuExist_AddsCocktailToMenu()
        {
            _mockCocktailRepository.Setup(x => x.GetBySerialNumberAsync(_event.SerialNumber)).ReturnsAsync(_cocktail);
            _mockCocktailMenuRepository.Setup(x => x.GetByStringIdAsync(_event.menuId)).ReturnsAsync(_cocktailMenu);
            var newMenuItem = new MenuItem(_cocktail, 5.5);
            _mockMenuItemRepository.Setup(x => x.addAsync(It.IsAny<MenuItem>())).Returns(Task.CompletedTask);
            _mockCocktailMenuRepository.Setup(x => x.saveChangesAsync()).Returns(Task.CompletedTask);

            await _eventHandler.Handle(_event);

            _mockCocktailMenuRepository.Verify(x => x.saveChangesAsync(), Times.Once());
            _mockMenuItemRepository.Verify(x => x.addAsync(It.IsAny<MenuItem>()), Times.Once());
            Assert.That(1,Is.EqualTo(_cocktailMenu.Cocktails.Count));
        }
    }
}
