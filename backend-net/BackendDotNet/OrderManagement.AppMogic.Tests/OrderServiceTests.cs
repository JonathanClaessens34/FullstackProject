using AppLogic.Events;
using Castle.Core.Resource;
using Moq;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic.Tests
{
    public class OrderServiceTests : TestBase
    {
        private OrderService _orderService;
        private Mock<IOrderRepository> _orderRepositoryMock;
        private Mock<ICocktailMenuRepository> _cocktailMenuRepositoryMock;
        private Mock<IMenuItemRepository> _menuItemRepositoryMock;
        private Mock<IOrderItemRepository> _orderItemRepositoryMock;
        private Mock<ICocktailRepository> _cocktailRepositoryMock;
        private Mock<IEventBus> _eventBusMock;

        [SetUp]
        public void Setup()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _cocktailMenuRepositoryMock = new Mock<ICocktailMenuRepository>();
            _menuItemRepositoryMock = new Mock<IMenuItemRepository>();
            _orderItemRepositoryMock= new Mock<IOrderItemRepository>();
            _cocktailRepositoryMock= new Mock<ICocktailRepository>();
            _eventBusMock = new Mock<IEventBus>();
            _orderService = new OrderService(_orderRepositoryMock.Object, _cocktailMenuRepositoryMock.Object, _menuItemRepositoryMock.Object, _eventBusMock.Object, _orderItemRepositoryMock.Object, _cocktailRepositoryMock.Object);
        }

        [Test]
        public async Task TestAddCocktailToOrderAsync()
        {
            // Arrange
            Guid orderId = Guid.NewGuid();
            String barId = Random.NextString();
            Guid menuItemId = Guid.NewGuid();
            string name = Random.NextString();
            string serialNumber = "1234567890123";
            Cocktail cocktail = new Cocktail(serialNumber, name, "www.url.be");
            double price = Random.NextDouble();
            Order order = new Order(barId);
            MenuItem menuItem = new MenuItem(cocktail, price);

            _orderRepositoryMock.Setup(r => r.GetById(orderId)).ReturnsAsync(order);
            _menuItemRepositoryMock.Setup(r => r.GetById(menuItemId)).ReturnsAsync(menuItem);

            // Act
            await _orderService.AddCocktailToOrderAsync(orderId, menuItemId);

            // Assert
            Assert.That(order.OrderItems.Count, Is.EqualTo(1));
            _orderRepositoryMock.Verify(r => r.CommitTrackedChangesAsync(), Times.Once());
        }

        [Test]
        public async Task TestDeleteCocktailFromOrderAsync()
        {
            // Arrange
            Guid orderId = Guid.NewGuid();
            Guid menuItemId = Guid.NewGuid();
            String barId = Random.NextString();
            string name = Random.NextString();
            string serialNumber = "1234567890123";
            Cocktail cocktail = new Cocktail(serialNumber, name, "www.url.be");
            double price = Random.NextDouble();
            Order order = new Order(barId);
            MenuItem menuItem = new MenuItem(cocktail, price);

            _orderRepositoryMock.Setup(x => x.GetById(orderId)).Returns(Task.FromResult(order));
            _menuItemRepositoryMock.Setup(x => x.GetById(menuItemId)).Returns(Task.FromResult(menuItem));
            _orderRepositoryMock.Setup(x => x.CommitTrackedChangesAsync()).Returns(Task.CompletedTask);

            // Act
            await _orderService.DeleteCocktailFromOrderAsync(orderId, menuItemId.ToString(), price);

            // Assert
            Assert.IsTrue(order.OrderItems.Count == 0);
            _orderRepositoryMock.Verify(x => x.CommitTrackedChangesAsync(), Times.Once());
        }

        [Test]
        public async Task TestDeleteOrderAsync()
        {
            // Arrange
            Guid orderId = Guid.NewGuid();
            String barId = Random.NextString();
            Order order = new Order(barId);

            _orderRepositoryMock.Setup(x => x.GetById(orderId)).Returns(Task.FromResult(order));
            _orderRepositoryMock.Setup(x => x.DeleteOrder(order)).Returns(Task.CompletedTask);

            // Act
            var result = _orderService.DeleteOrderAsync(orderId);

            // Assert
            _orderRepositoryMock.Verify(x => x.GetById(orderId), Times.Once);
            _orderRepositoryMock.Verify(x => x.DeleteOrder(order), Times.Once);
            Assert.IsTrue(result.IsCompleted);

        }

        [Test]
        public async Task FinelizeOrder_ShouldSetPayedToTrue()
        {
            //Arrange
            String barId = Random.NextString();
            Order order = new Order(barId);
            _orderRepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).ReturnsAsync(order);
            // Act tablenr:4
            var result = await _orderService.FinelizeOrder(order.Id,4);

            // Assert
            Assert.IsTrue(result.Payed);
        }

        [Test]
        public async Task FinelizeOrder_ShouldDiscountPriceForCustomer()
        {
            //Arrange
            string guidCustomer = Random.NextString();
            string guidBar = Random.NextString();
            Order order = new Order(guidCustomer,guidBar);
            order.TotalPrice = 100;
            _orderRepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).ReturnsAsync(order);
            // Act
            var result = await _orderService.FinelizeOrder(order.Id, 4);

            // Assert
            Assert.AreEqual(90, result.TotalPrice);
        }

        [Test]
        public async Task FinelizeOrder_ShouldNotDiscountPriceForNonCustomer()
        {
            //Arrange
            String barId = Random.NextString();
            Order order = new Order(barId);
            order.TotalPrice = 100;
            _orderRepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).ReturnsAsync(order);
            // Act
            var result = await _orderService.FinelizeOrder(order.Id, 4);

            // Assert
            Assert.AreEqual(100, result.TotalPrice);
        }

        [Test]
        public async Task FinelizeOrder_ShouldCommitTrackedChanges()
        {
            // Arrange
            String barId = Random.NextString();
            Order order = new Order(barId);
            _orderRepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).ReturnsAsync(order);
            // Act
            await _orderService.FinelizeOrder(order.Id, 4);

            // Assert
            _orderRepositoryMock.Verify(r => r.CommitTrackedChangesAsync(), Times.Once());
        }


        [Test]
        public async Task GetOrderHistory_ReturnsExpectedResult()
        {
            // Arrange
            string id = Random.NextString();
            Guid customerId = Guid.Parse(id);
            Order order = new Order(id);
            List<Order> expectedResult = new List<Order>();
            expectedResult.Add(order);
            _orderRepositoryMock.Setup(r => r.getOrderHistory(customerId)).Returns(Task.FromResult(expectedResult));

            // Act
            List<Order> result = await _orderService.GetOrderHistory(customerId);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TestInitializeNewOrderAsync_WithNull()
        {
            // Act
            var result = _orderService.InitializeNewOrderAsync(null, "0f8fad5b-d9cb-469f-a165-70867728950e").Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.CustomerId == null);
            _orderRepositoryMock.Verify(mock => mock.AddAsync(It.IsAny<Order>()), Times.Once);
        }

        [Test]
        public void TestInitializeNewOrderAsync_WithCustomerId()
        {
            //Arrange
            string guid = Random.NextString();
            String barId = Random.NextString();
            Guid customerId = Guid.Parse(guid);

            // Act
            var result = _orderService.InitializeNewOrderAsync(guid,barId).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.CustomerId == customerId);
            _orderRepositoryMock.Verify(mock => mock.AddAsync(It.IsAny<Order>()), Times.Once);
        }
    }
}
