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
    public class CustomerDbRepositoryTests :TestBase
    {
        private readonly DbContextOptions<OrderManagementContext> _dbContextOptions;
        private readonly OrderManagementContext _context;
        private readonly CustomerDbRepository _customerDbRepository;

        public CustomerDbRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<OrderManagementContext>()
                .UseInMemoryDatabase(databaseName: "Add_customer_to_repository")
                .Options;

            _context = new OrderManagementContext(_dbContextOptions);
            _customerDbRepository = new CustomerDbRepository(_context);
        }

        [Test]
        public async Task AddAsync_AddCustomerToDbContext()
        {
            //Arrange
            Customer testCustomer = new Customer("09a09c97-abbd-4793-80db-b7eb828c46fc", "customer1");

            //Act
            await _customerDbRepository.AddAsync(testCustomer);

            //Assert
            using (var context = new OrderManagementContext(_dbContextOptions))
            {
                var customers = await context.Customers.ToListAsync();
                NUnit.Framework.Assert.That(customers.Count, Is.EqualTo(1));
                NUnit.Framework.Assert.That(testCustomer, Is.EqualTo(customers[0]));
            }
        }
    }
}
