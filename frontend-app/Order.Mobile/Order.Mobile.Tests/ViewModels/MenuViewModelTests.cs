using Moq;
using Order.Mobile.Models;
using Order.Mobile.Services;
using Order.Mobile.Services.Backend;
using Order.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Mobile.Tests.ViewModels
{
    [TestFixture]
    public class MenuViewModelTests
    {
        private MenuViewModel _viewModel;
        private Mock<INavigationService> _mockNavigationService;
        private Mock<IOrderService> _mockOrderService;

        [SetUp]
        public void Setup()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _mockOrderService = new Mock<IOrderService>();

            _viewModel = new MenuViewModel(_mockNavigationService.Object, _mockOrderService.Object);
        }


        [Test]
        public void ExecuteAddItemToOrder_AddsItemToOrder()
        {
            // Arrange
            var item = new Models.MenuItem { Id = Guid.NewGuid() };
            _viewModel.OrderClass = new OrderClass { Id = Guid.NewGuid() };
            _mockOrderService.Setup(os => os.AddToOrderAsync(_viewModel.OrderClass.Id, item.Id)).Returns(Task.FromResult(new OrderClass { Id = _viewModel.OrderClass.Id, OrderItems = new ObservableCollection<OrderItem> { new OrderItem { Id = item.Id } } }));

            // Act
            _viewModel.AddToOrderCommand.Execute(item);

            // Assert
            _mockOrderService.Verify(os => os.AddToOrderAsync(_viewModel.OrderClass.Id, item.Id), Times.Once());
            Assert.AreEqual(1, _viewModel.OrderClass.OrderItems.Count);
            Assert.AreEqual(item.Id, _viewModel.OrderClass.OrderItems[0].Id);
        }

        [Test]
        public void ViewOrderCommand_DoesNotNavigate_WhenOrderClassIsNull()
        {
            // Arrange
            _viewModel.OrderClass = null;

            // Act
            _viewModel.ViewOrderCommand.Execute(null);

            // Assert
            _mockNavigationService.Verify(ns => ns.NavigateRelativeAsync("OrderPage"), Times.Never());
        }

        [Test]
        public void AddToOrderCommand_DoesNotAddItemToOrder_WhenOrderClassIsNull()
        {
            // Arrange
            var item = new Models.MenuItem { Id = Guid.NewGuid() };
            _viewModel.OrderClass = null;

            // Act
            _viewModel.AddToOrderCommand.Execute(item);

            // Assert
            _mockOrderService.Verify(os => os.AddToOrderAsync(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Never());
        }

        [Test]
        public void OnAppearing_DoesNotCreateNewOrder_WhenCocktailMenuIsNull()
        {
            // Arrange
            _viewModel.CocktailMenu = null;

            // Act
            _viewModel.OnAppearing();

            // Assert
            _mockOrderService.Verify(os => os.MakeNewOrderAsync(It.IsAny<string>()), Times.Never());
            Assert.IsNull(_viewModel.OrderClass);
        }

        [Test]
        public async Task ExecuteLoadOrderCommand_ShouldNavigateToOrderPage()
        {
            // Arrange
            _viewModel.OrderClass = new OrderClass();

            // Act
            _viewModel.ExecuteLoadOrderCommand();

            // Assert
            _mockNavigationService.Verify(x => x.NavigateRelativeAsync("OrderPage"), Times.Once);
            
        }

        [Test]
        public async Task ExecuteAddItemToOrder_ShouldAddItemToOrder()
        {
            // Arrange
            var menuItem = new Models.MenuItem { Id = Guid.NewGuid() };
            var expectedOrder = new OrderClass { Id = Guid.NewGuid() };
            _viewModel.OrderClass = new OrderClass { Id = expectedOrder.Id };
            _mockOrderService.Setup(x => x.AddToOrderAsync(expectedOrder.Id, menuItem.Id)).ReturnsAsync(expectedOrder);

            // Act
            _viewModel.ExecuteAddItemToOrder(menuItem);

            // Assert
            Assert.AreEqual(_viewModel.OrderClass, expectedOrder);
            _mockOrderService.Verify(x => x.AddToOrderAsync(expectedOrder.Id, menuItem.Id), Times.Once);
        }

        [Test]
        public async Task StartAsyncOperation_ShouldMakeNewOrderAndSetTotalPrice()
        {
            // Arrange
            var expectedOrder = new OrderClass { Id = Guid.NewGuid(), TotalPrice = 0.1 };
            _viewModel._cocktailMenu = new CocktailMenu { id = Guid.NewGuid() };
            _mockOrderService.Setup(x => x.MakeNewOrderAsync(_viewModel._cocktailMenu.id.ToString())).ReturnsAsync(expectedOrder);

            // Act
            _viewModel.StartAsyncOperation();

            // Assert
            Assert.AreEqual(_viewModel._orderClass, expectedOrder);
            _mockOrderService.Verify(x => x.MakeNewOrderAsync(_viewModel._cocktailMenu.id.ToString()), Times.Once);
            Assert.AreEqual(_viewModel._orderClass.TotalPrice, 0.1);
        }









    }


}
