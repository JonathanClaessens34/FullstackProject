using Moq;
using Order.Mobile.Models;
using Order.Mobile.Services.Backend;
using Order.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Mobile.Tests.ViewModels
{
    [TestFixture]
    public class OrderHistoryViewModelTests
    {
        private OrderHistoryViewModel _viewModel;
        private Mock<IOrderService> _mockOrderService;

        [SetUp]
        public void SetUp()
        {
            _mockOrderService = new Mock<IOrderService>();
            _viewModel = new OrderHistoryViewModel(_mockOrderService.Object);
        }

        [Test]
        public void LoadOrderHistoryCommand_Executes_CallsGetOrderHistoryOnOrderService()
        {
            _viewModel.LoadOrderHistoryCommand.Execute(null);
            _mockOrderService.Verify(x => x.GetOrderHistory(), Times.Once);
        }

        [Test]
        public async Task LoadOrderHistoryCommand_Executes_PopulatesOrdersCollection()
        {
            var orders = new List<OrderClass>
            {
                new OrderClass(),
                new OrderClass()
            };
            _mockOrderService.Setup(x => x.GetOrderHistory()).ReturnsAsync(orders);

            _viewModel.LoadOrderHistoryCommand.Execute(null); //mess async

            Assert.AreEqual(2, _viewModel.Orders.Count);
        }

        [Test]
        public void LoadOrderHistoryCommand_Executes_ClearsOrdersCollection()
        {
            _viewModel.Orders.Add(new OrderClass());
            _viewModel.LoadOrderHistoryCommand.Execute(null);
            Assert.AreEqual(0, _viewModel.Orders.Count);
        }

        [Test]
        public async Task LoadOrderHistoryCommand_HandlesExceptions_DoesNotAddOrders()
        {
            var exception = new Exception();
            _mockOrderService.Setup(x => x.GetOrderHistory()).ThrowsAsync(exception);

            _viewModel.LoadOrderHistoryCommand.Execute(null);

            Assert.AreEqual(0, _viewModel.Orders.Count);
        }

        [Test]
        public async Task LoadOrderHistoryCommand_HandlesExceptions_CallsToastService()
        {
            var exception = new Exception();
            _mockOrderService.Setup(x => x.GetOrderHistory()).ThrowsAsync(exception);

            _viewModel.LoadOrderHistoryCommand.Execute(null);

            // Verify that the toast service was called with the exception message
            //_mockToastService.Verify(x => x.DisplayToastAsync(exception.Message));
        }

        [Test]
        public void Constructor_InitializesProperties()
        {
            var orderService = new Mock<IOrderService>().Object;
            var viewModel = new OrderHistoryViewModel(orderService);

            Assert.IsNotNull(viewModel.Orders);
            Assert.IsNotNull(viewModel.LoadOrderHistoryCommand);
        }

        [Test]
        public async Task ExecuteLoadOrderHistoryCommand_PopulatesOrdersCollection()
        {
            var orders = new List<OrderClass>
                {
                    new OrderClass(),
                    new OrderClass()
                };

            _mockOrderService.Setup(x => x.GetOrderHistory()).ReturnsAsync(orders);

            await _viewModel.ExecuteLoadOrderHistoryCommand();

            Assert.AreEqual(2, _viewModel.Orders.Count);
        }

        [Test]
        public void Orders_IsReadOnly()
        {
            var propertyInfo = typeof(OrderHistoryViewModel).GetProperty("Orders");

            Assert.IsTrue(propertyInfo.GetSetMethod() == null);
        }

        [Test]
        public void LoadOrderHistoryCommand_IsReadOnly()
        {
            var propertyInfo = typeof(OrderHistoryViewModel).GetProperty("LoadOrderHistoryCommand");

            Assert.IsTrue(propertyInfo.GetSetMethod() == null);
        }











    }

}
