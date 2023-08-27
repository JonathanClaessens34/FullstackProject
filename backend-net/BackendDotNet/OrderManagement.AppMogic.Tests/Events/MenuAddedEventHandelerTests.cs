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
    public class MenuAddedEventHandelerTests: TestBase
    {
        private readonly Mock<ICocktailMenuRepository> _mockCocktailMenuRepository;
        private readonly Mock<ILogger<MenuAddedEventHandeler>> _mockLogger;
        private readonly MenuAddedIntegrationEvent _testEvent;
        private readonly MenuAddedEventHandeler _eventHandler;

        public MenuAddedEventHandelerTests()
        {
            _mockCocktailMenuRepository = new Mock<ICocktailMenuRepository>();
            _mockLogger = new Mock<ILogger<MenuAddedEventHandeler>>();
            _testEvent = new MenuAddedIntegrationEvent
            {
                menuId = "test_menu_id",
                BarName = "test_bar_name"
            };
            _eventHandler = new MenuAddedEventHandeler(_mockCocktailMenuRepository.Object, _mockLogger.Object);
        }


        [Test]
        public async Task Handle_DoesNotAddExistingMenuToRepository()
        {
            // Arrange
            _mockCocktailMenuRepository.Setup(repo => repo.GetByStringIdAsync(_testEvent.menuId))
                .ReturnsAsync(new CocktailMenu());

            // Act
            await _eventHandler.Handle(_testEvent);

            // Assert
            _mockCocktailMenuRepository.Verify(repo => repo.addAsync(It.IsAny<CocktailMenu>()), Times.Never());
        }
    }
}
