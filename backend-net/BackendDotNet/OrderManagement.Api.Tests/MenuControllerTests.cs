using Microsoft.AspNetCore.Mvc;
using Moq;
using OrderManagement.Api.Controllers;
using OrderManagement.AppLogic;
using OrderManagement.Domain;

using Test;

namespace OrderManagement.Api.Tests
{
    public class MenuControllerTests : TestBase
    {
        private Mock<ICocktailMenuService> _cocktailManeServiceMock;
        private Mock<ICocktailMenuRepository> _cocktailMenuRepositoryMock;
        private Mock<ICocktailRepository> _cocktailRepositoryMock;
        private Mock<IMenuItemRepository> _menuItemRepositoryMock;
        private MenuController _controller;

        [SetUp]
        public void Setup()
        {
            _cocktailManeServiceMock = new Mock<ICocktailMenuService>();
            _cocktailMenuRepositoryMock = new Mock<ICocktailMenuRepository>();
            _cocktailRepositoryMock = new Mock<ICocktailRepository>();
            _menuItemRepositoryMock= new Mock<IMenuItemRepository>();

            _controller = new MenuController(_cocktailMenuRepositoryMock.Object, _cocktailManeServiceMock.Object, _cocktailRepositoryMock.Object, _menuItemRepositoryMock.Object);
        }

        [Test]
        public async Task GetById_ShouldReturnMenuWithId()
        {
            //Arrange
            CocktailMenu testMenu = new CocktailMenu();

            Guid guid = testMenu.Id;
            _cocktailMenuRepositoryMock.Setup(repo => repo.GetByIdAsync(guid)).ReturnsAsync(testMenu);

            //Act
            var result = await _controller.GetMenuByID(guid);

            //Assert
            Assert.IsNotNull(result);
            _cocktailMenuRepositoryMock.Verify(repo => repo.GetByIdAsync(guid), Times.Once);
            Assert.AreEqual(testMenu, (result as OkObjectResult).Value);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task GetByIdNotFound_ShouldReturnMenuNotFound()
        {
            //Arrange
            CocktailMenu testMenu = new CocktailMenu();
            Guid guid = testMenu.Id;

            //Act
            var result = await _controller.GetMenuByID(guid);

            //Assert
            Assert.IsNotNull(result);
            _cocktailMenuRepositoryMock.Verify(repo => repo.GetByIdAsync(guid), Times.Once);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task GetMenuitemByID_ReturnsOkResult_ForExistingMenuItem()
        {
            // Arrange
            var testId = Guid.NewGuid();
            Cocktail cocktail = new Cocktail("1234567891011", "test", "image");
            MenuItem menuItem = new MenuItem(cocktail, 2.0);
            _menuItemRepositoryMock.Setup(repo => repo.GetById(menuItem.Id))
                .ReturnsAsync(menuItem);

            // Act
            var result = await _controller.GetMenuitemByID(menuItem.Id);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.That(menuItem, Is.EqualTo((result as OkObjectResult).Value));
        }

        [Test]
        public async Task GetMenuitemByID_ReturnsNotFoundResult_ForNonExistentMenuItem()
        {
            // Arrange
            var testId = Guid.NewGuid();
            _menuItemRepositoryMock.Setup(repo => repo.GetById(testId))
                .ReturnsAsync((MenuItem?)null);

            // Act
            var result = await _controller.GetMenuitemByID(testId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task GetAll_ShouldReturnListOfMenus()
        {
            //Arrange
            CocktailMenu Menu1 = new CocktailMenu();
            CocktailMenu Menu2 = new CocktailMenu();
            CocktailMenu Menu3 = new CocktailMenu();
            List<CocktailMenu?> menus = new List<CocktailMenu>();
            menus.Add(Menu1); menus.Add(Menu2); menus.Add(Menu3);
            _cocktailMenuRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(menus);

            //Act
            var result = await _controller.GetAllMenus() as ObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsTrue(result.Value is List<CocktailMenu>);
            var returnedMenus = (List<CocktailMenu>)result.Value;
            Assert.AreEqual(menus.Count, returnedMenus.Count);
            Assert.AreEqual(menus[0].Id, returnedMenus[0].Id);
            Assert.AreEqual(menus[1].Id, returnedMenus[1].Id);
        }

        [Test]
        public async Task GetAll_ShouldReturnListEmpty()
        {
            //Arrange
            CocktailMenu Menu1 = new CocktailMenu();
            CocktailMenu Menu2 = new CocktailMenu();
            CocktailMenu Menu3 = new CocktailMenu();
            List<CocktailMenu?> menus = new List<CocktailMenu>();
            menus.Add(Menu1); menus.Add(Menu2); menus.Add(Menu3);

            //Act
            var result = await _controller.GetAllMenus();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }


    }
}
