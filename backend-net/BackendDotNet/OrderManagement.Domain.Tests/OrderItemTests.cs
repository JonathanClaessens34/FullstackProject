using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test;

namespace OrderManagement.Domain.Tests
{
    public class OrderItemTests :TestBase
    {
        [Test]
        public void Constructor_WithNothing_ShouldNotInitialize()
        {
            //Act 
            OrderItem orderItem = new OrderItem();

            //Assert
            Assert.That(orderItem.Id.ToString(), Is.EqualTo("00000000-0000-0000-0000-000000000000"));

        }

        [Test]
        public void Constructor_WithMenuItem_ShouldNotInitialize()
        {
            //Act 
            Cocktail cocktail = new Cocktail("1234567891011", "test", "image");
            MenuItem menuItem = new MenuItem(cocktail, 2.0);
            OrderItem orderItem = new OrderItem(2, menuItem);

            //Assert
            Assert.NotNull(orderItem.Id);
            Assert.That(orderItem.Amount, Is.EqualTo(2));
            Assert.That(orderItem.Cocktail, Is.EqualTo(cocktail));
            Assert.That(orderItem.Price, Is.EqualTo(2.0));
        }
    }
}
