using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Infrastructure
{
    internal class OrderManagementContext: DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<CocktailMenu> Menus { get; set; }
        public DbSet<Cocktail> Cocktails { get; set; }

        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public OrderManagementContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new MenuConfiguration().Configure(modelBuilder.Entity<CocktailMenu>());
            new OrderConfiguration().Configure(modelBuilder.Entity<Order>());
            new CocktailConfiguration().Configure(modelBuilder.Entity<Cocktail>());
            new MenuItemConfiguration().Configure(modelBuilder.Entity<MenuItem>());
            new OrderItemConfiguration().Configure(modelBuilder.Entity<OrderItem>());
            new CustomerConfiguration().Configure(modelBuilder.Entity<Customer>());
        }


    }
}
