using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test;

namespace OrderManagement.Domain.Tests
{
    public class CocktailTests : TestBase
    {
        private string _serialNumberString;
        private SerialNumber _serialNumber;
        private string _name;

        [SetUp]
        public void BeforeEachTest()
        {
            _serialNumberString = "1234567891011";
            _serialNumber = new SerialNumber(_serialNumberString);
            _name= Random.NextString();
        }

        [Test]
        public void Constructor_WithSerialNumberAndId_ShouldInitializeProperly()
        {
            //Act 
            Cocktail cocktail = new Cocktail(_serialNumberString, _name, "www.url.be");

            //Assert
            Assert.That(cocktail.Name, Is.EqualTo(_name));
            Assert.That(cocktail.SerialNumber, Is.EqualTo(_serialNumber));
        }

        [Test]
        public void CreateNew_ValidInput_ShouldInitializeFieldsCorrectly() 
        {
            //Act
            Cocktail cocktail = Cocktail.CreateNew(_serialNumberString, _name, "www.url.be");

            //Assert
            Assert.That(cocktail.SerialNumber, Is.EqualTo(_serialNumber));
            Assert.That(cocktail.Name, Is.EqualTo(_name));
        }

    }
}
