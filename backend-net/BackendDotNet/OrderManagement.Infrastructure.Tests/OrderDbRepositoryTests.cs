using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test;

namespace OrderManagement.Infrastructure.Tests
{
    public class OrderDbRepositoryTests: TestBase
    {
        private readonly DbContextOptions<OrderManagementContext> _dbContextOptions;
        private readonly OrderManagementContext _context;
        private readonly OrderDbRepository _orderDbRepository;

        public OrderDbRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<OrderManagementContext>()
                .UseInMemoryDatabase(databaseName: "Add_customer_to_repository")
                .Options;

            _context = new OrderManagementContext(_dbContextOptions);
            _orderDbRepository = new OrderDbRepository(_context);
        }

        [Test]
        public async Task AddAsync_AddOrderToDbContext()
        {
            //Arrange
            Order order = new Order();

            //Act
            await _orderDbRepository.AddAsync(order);

            //Assert
            var result = await _context.Orders.FindAsync(order.Id);
            Assert.That(order, Is.EqualTo(result));
        }
    }
}
