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
    public class MenuItemDbRepositoryTests : TestBase
    {
        private readonly DbContextOptions<OrderManagementContext> _dbContextOptions;
        private readonly OrderManagementContext _context;
        private readonly MenuItemDbRepository _menuItemDbRepository;

        public MenuItemDbRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<OrderManagementContext>()
                .UseInMemoryDatabase(databaseName: "Add_customer_to_repository")
                .Options;

            _context = new OrderManagementContext(_dbContextOptions);
            _menuItemDbRepository = new MenuItemDbRepository(_context);
        }

        [Test]
        public async Task AddAsync_AddMenuItemToDbContext()
        {
            //Arrange
            Cocktail cocktail = new Cocktail("1234567891011", "test", "image");
            MenuItem menuItem = new MenuItem(cocktail, 2.0);

            //Act
            await _menuItemDbRepository.addAsync(menuItem);

            //Assert
            var result = await _context.MenuItems.FindAsync(menuItem.Id);
            Assert.That(menuItem, Is.EqualTo(result));
        }
    }
}
