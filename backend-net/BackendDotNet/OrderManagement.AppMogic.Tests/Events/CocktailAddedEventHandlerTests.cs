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
    public class CocktailAddedEventHandlerTests: TestBase
    {
        private readonly Mock<ICocktailRepository> _mockCocktailRepository;
        private readonly Mock<ILogger<CocktailAddedEventHandler>> _mockLogger;
        private readonly CocktailAddedEventHandler _eventHandler;
        private readonly CocktailAddedIntegrationEvent _event;

        public CocktailAddedEventHandlerTests()
        {
            _mockCocktailRepository = new Mock<ICocktailRepository>();
            _mockLogger = new Mock<ILogger<CocktailAddedEventHandler>>();
            _eventHandler = new CocktailAddedEventHandler(_mockCocktailRepository.Object, _mockLogger.Object);
            _event = new CocktailAddedIntegrationEvent { SerialNumber = "1234567891011", Name = "Mojito", ImageUrl = "www.mojito.com/image.jpg" };
        }

        [Test]
        public async Task Handle_CocktailDoesNotExist_AddsCocktail()
        {
            _mockCocktailRepository.Setup(x => x.GetBySerialNumberAsync(_event.SerialNumber)).ReturnsAsync((Cocktail)null);

            await _eventHandler.Handle(_event);

            _mockCocktailRepository.Verify(x => x.AddAsync(It.IsAny<Cocktail>()), Times.Once());
            _mockCocktailRepository.Verify(x => x.CommitTrackedChangesAsync(), Times.Never());
        }
    }
}

