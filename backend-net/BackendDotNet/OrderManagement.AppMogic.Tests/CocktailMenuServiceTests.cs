using Moq;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic.Tests
{
    public class CocktailMenuServiceTests : TestBase
    {
        private Mock<ICocktailMenuRepository> _menuRepositoryMock;
        private CocktailMenuService _service;

        [SetUp]
        public void Setup()
        {
            _menuRepositoryMock = new Mock<ICocktailMenuRepository>();
            _service = new CocktailMenuService(_menuRepositoryMock.Object);
        }

        [Test]
        public async Task GetByIdAsync_Should_RetrieveCocktailMenu_WithTheId()
        {
            // Arrange
            string id = Random.NextString();
            string barName = Random.NextString();
            CocktailMenu cocktailMenu = new CocktailMenu(id, barName);
            _menuRepositoryMock.Setup(repo => repo.GetByIdAsync(cocktailMenu.Id)).ReturnsAsync(cocktailMenu);

            // Act
            CocktailMenu result = _service.MenuRepository.GetByIdAsync(cocktailMenu.Id).Result ;

            // Assert
            _menuRepositoryMock.Verify(repo => repo.GetByIdAsync(cocktailMenu.Id), Times.Once);
            Assert.That(cocktailMenu,Is.EqualTo(result));
        }

        [Test]
        public async Task GetByStringIdAsync_Should_RetrieveCocktailMenu_WithTheId()
        {
            // Arrange
            string id = Random.NextString();
            string barName = Random.NextString();
            CocktailMenu cocktailMenu = new CocktailMenu(id, barName);
            _menuRepositoryMock.Setup(repo => repo.GetByStringIdAsync(id)).ReturnsAsync(cocktailMenu);

            // Act
            CocktailMenu result = _service.MenuRepository.GetByStringIdAsync(id).Result;

            // Assert
            _menuRepositoryMock.Verify(repo => repo.GetByStringIdAsync(id), Times.Once);
            Assert.That(cocktailMenu, Is.EqualTo(result));
        }

        [Test]
        public async Task GetAllAsync_Should_RetrieveAListOffAllTheCocktailMenus()
        {
            // Arrange
            CocktailMenu cocktailMenu1 = new Mock<CocktailMenu>().Object;
            CocktailMenu cocktailMenu2 = new Mock<CocktailMenu>().Object;
            CocktailMenu cocktailMenu3 = new Mock<CocktailMenu>().Object;
            List<CocktailMenu> cocktailMenus = new List<CocktailMenu>();
            cocktailMenus.Add(cocktailMenu1); cocktailMenus.Add(cocktailMenu2); cocktailMenus.Add(cocktailMenu3);
            _menuRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(cocktailMenus);

            // Act
            List<CocktailMenu> result = _service.MenuRepository.GetAll().Result;

            // Assert
            _menuRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            Assert.That(cocktailMenus, Is.EqualTo(result));
        }

        [Test]
        public async Task GetCocktailBySerialNumberAsync_Should_RetrieveTheCocktail_WithTheSerialNumber()
        {
            // Arrange
            string name = Random.NextString();
            string serialNumber = "1234567890123";
            Cocktail cocktail = new Cocktail(serialNumber, name, "www.url.be");
            CocktailMenu cocktailMenu = new Mock<CocktailMenu>().Object;
            _menuRepositoryMock.Setup(repo => repo.GetCocktailBySerialNumberAsync(cocktailMenu.Id, cocktail.SerialNumber)).ReturnsAsync(cocktail);

            // Act
            var result = await _menuRepositoryMock.Object.GetCocktailBySerialNumberAsync(cocktailMenu.Id, cocktail.SerialNumber);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(cocktail.SerialNumber, Is.EqualTo(result.SerialNumber));
        }

    }
}
