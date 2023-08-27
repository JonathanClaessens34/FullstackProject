using Moq;
using Order.Mobile.Models;
using Order.Mobile.Services;
using Order.Mobile.Services.Backend;
using Order.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Mobile.Tests.ViewModels
{
    public class CocktailMenusViewModelTests
    {

        private Mock<ICocktailMenuService> mockCocktailMenuService;
        private Mock<IToastService> mockToastService;
        private Mock<INavigationService> mockNavigationService;
        private CocktailMenusViewModel _model;

        [SetUp]
        public void BeforeEachTest()
        {
            mockCocktailMenuService = new Mock<ICocktailMenuService>();
            mockToastService = new Mock<IToastService>();
            mockNavigationService = new Mock<INavigationService>();

           _model = new CocktailMenusViewModel(
                mockCocktailMenuService.Object,
                mockToastService.Object,
                mockNavigationService.Object);
        }

        [Test]
        public async Task ExecuteLoadCocktailMenusCommand_ShouldLoadCocktailMenus()
        {
            // Arrange
            var menus = new List<CocktailMenu>
            {
                new CocktailMenu { id = Guid.NewGuid(), barName = "Menu 1" },
                new CocktailMenu { id = Guid.NewGuid(), barName = "Menu 2" }
            };

            
            mockCocktailMenuService.Setup(x => x.GetAllMenusAsync()).ReturnsAsync(menus);


            // Act
            _model.LoadMenusCommand.Execute(null); //mess await

            // Assert
            Assert.AreEqual(2, _model.Items.Count);
            Assert.AreEqual(menus[0].id, _model.Items[0].id);
            Assert.AreEqual(menus[0].barName, _model.Items[0].barName);
            Assert.AreEqual(menus[1].id, _model.Items[1].id);
            Assert.AreEqual(menus[1].barName, _model.Items[1].barName);
            mockCocktailMenuService.Verify(x => x.GetAllMenusAsync(), Times.Once);
        }

        [Test]
        public async Task OnCocktailMenuSelected_ShouldNavigateAndSendMessage()
        {
            // Arrange
            var menu = new CocktailMenu { id = Guid.NewGuid(), barName = "Menu 1" };

            // Act
            _model.OnCocktailMenuSelected(menu);

            // Assert
            mockNavigationService.Verify(x => x.NavigateRelativeAsync("MenuPage"), Times.Once);
            //mockNavigationService.Verify(x => x.Send(this, "MenuSelected", menu), Times.Once);
        }


        [Test]
        public async Task ExecuteLoadCocktailMenusCommand_ShouldLoadAllCocktailMenus()
        {
            // Arrange

            var expectedCocktailMenus = new List<CocktailMenu>()
                {
                     new CocktailMenu { id = Guid.NewGuid(), barName = "Menu 1" },
                    new CocktailMenu { id = Guid.NewGuid(), barName = "Menu 2" }
                };

            mockCocktailMenuService.Setup(x => x.GetAllMenusAsync()).ReturnsAsync(expectedCocktailMenus);

            // Act
            await _model.ExecuteLoadCocktailMenusCommand();

            // Assert
            Assert.AreEqual(_model.Items.Count, expectedCocktailMenus.Count);
            mockToastService.Verify(x => x.DisplayToastAsync(It.IsAny<string>()), Times.Never);
            mockCocktailMenuService.Verify(x => x.GetAllMenusAsync(), Times.Once);
        }



    }
}
