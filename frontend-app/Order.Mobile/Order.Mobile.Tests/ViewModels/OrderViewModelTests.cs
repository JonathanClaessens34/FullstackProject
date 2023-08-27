using CommunityToolkit.Maui.Core.Extensions;
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
    public class OrderViewModelTests
    {

        private Mock<IOrderService> _orderService;
        private Mock<IToastService> _toastService;

        private OrderViewModel _model;

        [SetUp]
        public void BeforeEachTest()
        {
            _orderService = new Mock<IOrderService>(); 
            _toastService = new Mock<IToastService>();

            _model = new OrderViewModel(_orderService.Object, _toastService.Object);
        }


        [Test]
        public void ExecuteDeleteItemFromOrder_RemovesItemFromOrderAndUpdatesOrderItems()
        {
            // Arrange

            var orderItem = new OrderItem { Cocktail = new Cocktail { SerialNumber = new SerialNumber { Nummer = "1234567890123" } }, Price = 5 };
            _model.OrderItems.Add(orderItem);
            _model.OrderClass = new OrderClass { Id = Guid.NewGuid(), OrderItems = _model.OrderItems.ToObservableCollection() };
            OrderClass returndOrder = new OrderClass { Id = _model.OrderClass.Id, OrderItems = new ObservableCollection<OrderItem>() };
            _orderService.Setup(x => x.DeleteFromOrderAsync(_model.OrderClass.Id, "1234567890123", 5)).ReturnsAsync(returndOrder);

            // Act
            _model.ExecuteDeleteItemFromOrder(orderItem);

            // Assert
            Assert.AreEqual(0, _model.OrderItems.Count);
            _orderService.Verify(x => x.DeleteFromOrderAsync(_model.OrderClass.Id, "1234567890123", 5), Times.Once);
        }


        [Test]
        public void ExecuteFinalizeOrder_FinalizesOrderAndDisplaysToastIfTableIsNotZero()
        {
            // Arrange
            _model.OrderClass = new OrderClass { Id = Guid.NewGuid(), Table = 1 };
            _orderService.Setup(x => x.FinalizeOrderAsync(It.IsAny<Guid>(), 1));

            // Act
            _model.ExecuteFinalizeOrder().Wait();

            // Assert
            _orderService.Verify(x => x.FinalizeOrderAsync(It.IsAny<Guid>(), 1), Times.Once);
            _toastService.Verify(x => x.DisplayToastAsync(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void ExecuteFinalizeOrder_DoesntFinalizeOrderAndDisplaysToastIfTableIsZero()
        {
            // Arrange
            _model.OrderClass = new OrderClass { Id = Guid.NewGuid(), Table = 0 };

            // Act
            _model.ExecuteFinalizeOrder().Wait();

            // Assert
            _orderService.Verify(x => x.FinalizeOrderAsync(It.IsAny<Guid>(), It.IsAny<int>()), Times.Never);
            _toastService.Verify(x => x.DisplayToastAsync("TableNr cant be 0"), Times.Once);
        }

        [Test]
        public void FinalizeOrderCommand_ExecutesFinalizeOrder()
        {
            // Arrange
            _model.OrderClass = new OrderClass { Id = Guid.NewGuid(), Table = 1 };
            _orderService.Setup(x => x.FinalizeOrderAsync(It.IsAny<Guid>(), 1));

            // Act
            _model.FinalizeOrderCommand.Execute(null);

            // Assert
            _orderService.Verify(x => x.FinalizeOrderAsync(It.IsAny<Guid>(), 1), Times.Once);
        }


        [Test]
        public void DeleteFromOrderCommand_ExecutesDeleteFromOrder()
        {
            // Arrange
            var orderItem = new OrderItem { Cocktail = new Cocktail { SerialNumber = new SerialNumber { Nummer = "1234567890123" } }, Price = 5 };
            _model.OrderItems.Add(orderItem);
            _model.OrderClass = new OrderClass { Id = Guid.NewGuid(), OrderItems = _model.OrderItems.ToObservableCollection() };
            OrderClass returndOrder = new OrderClass { Id = _model.OrderClass.Id, OrderItems = new ObservableCollection<OrderItem>() };
            _orderService.Setup(x => x.DeleteFromOrderAsync(It.IsAny<Guid>(), "1234567890123", 5)).ReturnsAsync(returndOrder);

            // Act
            _model.DeleteFromOrderCommand.Execute(orderItem);

            // Assert
            Assert.AreEqual(0, _model.OrderItems.Count);
            _orderService.Verify(x => x.DeleteFromOrderAsync(It.IsAny<Guid>(), "1234567890123", 5), Times.Once);
        }


        [Test]
        public void OnAppearing_ClearsOrderItemsWhenOrderSelectedIsSent()
        {
            // Arrange
            var orderItems = new ObservableCollection<OrderItem>
            {
                new OrderItem { Cocktail = new Cocktail { SerialNumber = new SerialNumber { Nummer = "1234567890123" } }, Price = 5 },
                new OrderItem { Cocktail = new Cocktail { SerialNumber = new SerialNumber { Nummer = "2345678901234" } }, Price = 7 }
            };
            var openOrder = new OrderClass { Id = Guid.NewGuid(), OrderItems = orderItems };
            MessagingCenter.Send(_model, "OrderSelected", openOrder);

            // Act
            openOrder = new OrderClass { Id = Guid.NewGuid() };
            MessagingCenter.Send(_model, "OrderSelected", openOrder);

            // Assert
            Assert.AreEqual(0, _model.OrderItems.Count);
        }




    }
}
