using OrderManagement.Domain.Tests.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test;

namespace OrderManagement.Domain.Tests
{
    public class CocktailMenuTests : TestBase
    {
        private Guid _id;
        private string _barName;
        private double _price;
        private string _menuId;

        [SetUp]
        public void BeforeEachTest()
        {
            _menuId = Random.NextString();
            _id = Guid.Parse(_menuId);
            _barName = Random.NextString();
            _price = Random.NextDouble();
        }

        [Test]
        public void Constructor_Empty_ShouldInitializeProperly() 
        {
            //Act 
            CocktailMenu cocktailMenu= new CocktailMenu();

            //Assert
            Assert.NotNull(cocktailMenu.Id);
            Assert.NotNull(cocktailMenu.BarName);
            Assert.NotNull(cocktailMenu.Cocktails);
        }

        [TestCase("bar1")]
        [TestCase("bar2")]
        public void Constructor_WithBarName_ShouldInitializeProperly(string barName)
        {
            //Act 
            CocktailMenu cocktailMenu = new CocktailMenu(barName);

            //Assert
            Assert.NotNull(cocktailMenu.Id);
            Assert.That(cocktailMenu.BarName, Is.EqualTo(barName));
            Assert.NotNull(cocktailMenu.Cocktails);
        }

        [TestCase("bar1", "0f8fad5b-d9cb-469f-a165-70867728950e")]
        [TestCase("bar2", "0f8fad5b-a5ed-7b95-a165-70867728950e")]
        public void Constructor_WithBarNameAndId_ShouldInitializeProperly(string barName, string id)
        {
            //Act 
            CocktailMenu cocktailMenu = new CocktailMenu(id, barName);
            Guid guid = new Guid(id);

            //Assert
            Assert.That(cocktailMenu.Id.ToString, Is.EqualTo(id));
            Assert.That(cocktailMenu.BarName, Is.EqualTo(barName));
            Assert.NotNull(cocktailMenu.Cocktails);
        }

        [Test]
        public void CreateNew_ValidInput_ShouldInitializeFieldsCorrectly()
        {
            //Act
            CocktailMenu cocktailMenu = CocktailMenu.CreateNew(_menuId, _barName);

            //Assert
            Assert.That(cocktailMenu.Id, Is.EqualTo(_id));
            Assert.That(cocktailMenu.BarName, Is.EqualTo(_barName));
        }

        [TestCase(null)]
        [TestCase("")]
        public void CreateNew_EmptyId_ShouldThrowContractException(string emptyId)
        {
            Assert.That(() => CocktailMenu.CreateNew(emptyId, _barName), Throws.InstanceOf<Exception>());
        }

        [Test]
        public void AddCocktail_CocktailNotInMenu_ShouldAddCocktail()
        {
            //Arrange
            CocktailMenu cocktailMenu = new CocktailMenu(_barName);
            Cocktail cocktail = new CocktailBuilder().Build();
            MenuItem menuItem = new MenuItem(cocktail, _price);

            //Act
            cocktailMenu.AddCocktail(menuItem);

            //Assert
            Assert.That(cocktailMenu.Cocktails, Has.Count.EqualTo(1));
            Assert.That(cocktailMenu.Cocktails.First().Cocktail, Is.EqualTo(cocktail));
            Assert.That(cocktailMenu.Cocktails.First().Price, Is.EqualTo(_price));
        }

        [Test]
        public void FindCocktailBySerialnumber_CocktailInMenu_ShouldReturnCocktail()
        {
            //Arrange
            CocktailMenu cocktailMenu = new CocktailMenu(_barName);
            Cocktail cocktail = new CocktailBuilder().Build();
            MenuItem menuItem = new MenuItem(cocktail, _price);

            //Act
            cocktailMenu.AddCocktail(menuItem);
            Cocktail foundCocktail = cocktailMenu.FindBySerialnumber(cocktail.SerialNumber);

            //Assert
            Assert.IsNotNull(foundCocktail);
            Assert.That(foundCocktail, Is.EqualTo(cocktail));
        }

        [Test]
        public void FindCocktailBySerialnumber_CocktailNotInMenu_ShouldReturnException()
        {
            //Arrange
            CocktailMenu cocktailMenu = new CocktailMenu(_barName);
            Cocktail cocktail = new CocktailBuilder().Build();

            //Assert
            Assert.That(() =>cocktailMenu.FindBySerialnumber(cocktail.SerialNumber), Throws.InstanceOf<Exception>()); 
        }
    }
}
