using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test;

namespace OrderManagement.Domain.Tests
{
    public class OrderTests : TestBase
    {
        [Test]
        public void Constructor_WithoutId_ShouldInitialize()
        {
            //Act 
            Order order = new Order();

            //Assert
            Assert.NotNull(order.Id);
            Assert.NotNull(order.OrderItems);
            Assert.That(order.TotalPrice, Is.EqualTo(0));
        }

        [TestCase("0f8fad5b-d9cb-469f-a165-70867728950b")]
        [TestCase("0f8fad5b-d9cb-469f-a165-70867728950a")]
        public void Constructor_WithBarrId_ShouldInitialize(string barId)
        {
            //Act 
            Order order = new Order(barId);

            //Assert
            Assert.NotNull(order.Id);
            Assert.NotNull(order.OrderItems);
            Assert.That(order.TotalPrice, Is.EqualTo(0));
        }


        [TestCase("0f8fad5b-d9cb-469f-a165-70867728950e", "0f8fad5b-d9cb-469f-a165-70867728950b")]
        [TestCase("0f8fad5b-a5ad-7b95-a165-70867728950e", "0f8fad5b-d9cb-469f-a165-70867728950a")]
        public void Constructor_WithCustomerId_ShouldInitializeWithDiscount(string customerId,string barId)
        {
            //Act 
            Order order = new Order(customerId,barId);

            //Assert
            Assert.NotNull(order.Id);
            Assert.NotNull(order.OrderItems);           
            Assert.That(order.TotalPrice, Is.EqualTo(0));  
        }


        [TestCase(null, "0f8fad5b-d9cb-469f-a165-70867728950e")]
        [TestCase("", "0f8fad5b-d9cb-469f-a165-70867728950e")]
        public void Constructor_WithCustomerIdNull_ShouldInitializeWithoutDiscount(string emptyId, string emptyBarId)
        {
            //Act 
            Order order = new Order(emptyId,emptyBarId);

            //Assert
            Assert.NotNull(order.Id);
            Assert.NotNull(order.OrderItems);
            Assert.That(order.TotalPrice, Is.EqualTo(0));
        }

        [Test]
        public void AddOrder_ShouldAddOrderToOrder()
        {
            //Act 
            Order order = new Order();
            Cocktail cocktail = new Cocktail("1234567891011","test","image");
            MenuItem menuItem = new MenuItem(cocktail, 2.0);
            OrderItem orderItem= new OrderItem(2, menuItem);

            order.AddCocktail(orderItem);
            //Assert
            Assert.NotNull(order.Id);
            Assert.That(order.OrderItems.Contains(orderItem));
        }

        [Test]
        public void DeleteOrder_ShouldRemoveOrderFromOrder()
        {
            //Act 
            Order order = new Order();
            Cocktail cocktail = new Cocktail("1234567891011", "test", "image");
            MenuItem menuItem = new MenuItem(cocktail, 2.0);
            OrderItem orderItem = new OrderItem(1, menuItem);

            order.AddCocktail(orderItem);
            order.DeleteCocktail("1234567891011");
            //Assert
            Assert.NotNull(order.Id);
            Assert.That(order.OrderItems.Count(), Is.EqualTo(0));
        }
    }
}
