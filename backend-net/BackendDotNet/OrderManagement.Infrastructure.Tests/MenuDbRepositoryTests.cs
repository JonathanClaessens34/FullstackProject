using Castle.Core.Resource;
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
    public class MenuDbRepositoryTests :TestBase
    {

        private readonly DbContextOptions<OrderManagementContext> _dbContextOptions;
        private readonly OrderManagementContext _context;
        private readonly MenuDbRepository _menuDbRepository;

        public MenuDbRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<OrderManagementContext>()
                .UseInMemoryDatabase(databaseName: "Add_menu_to_repository")
                .Options;

            _context = new OrderManagementContext(_dbContextOptions);
            _menuDbRepository = new MenuDbRepository(_context);
        }

        [Test]
        public async Task addAsync_AddsMenuToDbContext()
        {
            //Arrange
           
            CocktailMenu testMenu = new CocktailMenu ("bar1");

            //Act
            await _menuDbRepository.addAsync(testMenu);

            //Assert
            using (var context = new OrderManagementContext(_dbContextOptions))
            {
                var menus = await context.Menus.ToListAsync();
                NUnit.Framework.Assert.That(menus.Count, Is.EqualTo(1));
                NUnit.Framework.Assert.That(testMenu, Is.EqualTo(menus[0]));
            }
        }
    }
}
