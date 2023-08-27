using Castle.Core.Resource;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OrderManagement.Api.Controllers;
using OrderManagement.AppLogic;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test;

namespace OrderManagement.Api.Tests
{
    public class OrderControllerTests : TestBase
    {
        private Mock<IOrderService> _orderServiceMock;
        private Mock<IOrderRepository> _orderRepositoryMock;
        private OrderController _controller;

        [SetUp]
        public void Setup()
        {
            _orderServiceMock = new Mock<IOrderService>();
            _orderRepositoryMock = new Mock<IOrderRepository>();

            _controller = new OrderController(_orderServiceMock.Object, _orderRepositoryMock.Object);
        }

        [Test]
        public async Task TestInitializeNewOrder()
        {
            // Arrange
            String customerId = Random.NextString();
            String barId = Random.NextString();
            Order testOrder = new Order(customerId, barId);
            _orderServiceMock.Setup(service => service.InitializeNewOrderAsync(customerId, barId)).ReturnsAsync(testOrder);

            // Act
            var result = await _controller.InitializeNewOrder(customerId, barId) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(Guid.Parse(customerId), ((Order)result.Value).CustomerId);
        }

        [Test]
        public async Task AddCocktailToOrder_ShouldReturnOk()
        {
            // Arrange
            Guid orderId = Guid.NewGuid();
            Guid menuItemId = Guid.NewGuid();
            String customerId = Random.NextString();
            String barId = Random.NextString();
            Order testOrder = new Order(customerId, barId);
            _orderServiceMock.Setup(service => service.AddCocktailToOrderAsync(orderId, menuItemId)).ReturnsAsync(testOrder);

            // Act
            var result = await _controller.AddCocktailToOrder(orderId, menuItemId) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOf<OkObjectResult>(result);
            _orderServiceMock.Verify(repo => repo.AddCocktailToOrderAsync(orderId, menuItemId), Times.Once);
        }

        [Test]
        public async Task DeleteOrder_ValidInput_ReturnsOkResult()
        {
            // Arrange
            Guid orderId = Guid.NewGuid();

            // Act
            var result = await _controller.DeleteOrder(orderId) as OkResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task FinelizeOrder_ValidInput_ReturnsOkResult()
        {
            // Arrange 5 is random gekoze
            Guid orderId = Guid.NewGuid();

            // Act
            var result = await _controller.FinelizeOrder(orderId,5) as OkResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOf<OkResult>(result);
        }
        [Test]
        public async Task GetOrderHistory_ValidInput_ReturnsOkResult()
        {
            // Arrange
            string customerIdString = Random.NextString();
            Guid customerId = Guid.Parse(customerIdString);
            List<Order> expectedOrders = new List<Order>()
            {
                new Order(customerIdString) ,
                new Order(customerIdString),
                new Order(customerIdString)
            };
            _orderServiceMock.Setup(service => service.GetOrderHistory(customerId)).ReturnsAsync(expectedOrders);

            // Act
            var result = await _controller.GetOrderHistory(customerId)as OkObjectResult;

            // Assert         
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}
