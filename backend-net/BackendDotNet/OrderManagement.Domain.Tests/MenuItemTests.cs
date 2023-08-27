using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test;

namespace OrderManagement.Domain.Tests
{
    public class MenuItemTests : TestBase
    {

        [Test]
        public void Constructor_WithNothing_ShouldNotInitialize()
        {
            //Act 
            MenuItem menuItem = new MenuItem();

            //Assert
            Assert.That(menuItem.Id.ToString(), Is.EqualTo("00000000-0000-0000-0000-000000000000"));
            Assert.That(menuItem.Cocktail, Is.EqualTo(null));
            Assert.That(menuItem.Price, Is.EqualTo(0));
        }

        [Test]
        public void Constructor_WithCocktail_ShouldNotInitialize()
        {
            //Act 
            Cocktail cocktail = new Cocktail("1234567891011", "test", "image");
            MenuItem menuItem = new MenuItem(cocktail, 2.0);

            //Assert
            Assert.NotNull(menuItem.Id);
            Assert.That(menuItem.Cocktail, Is.EqualTo(cocktail));
            Assert.That(menuItem.Price, Is.EqualTo(2.0));
        }
    }
}
