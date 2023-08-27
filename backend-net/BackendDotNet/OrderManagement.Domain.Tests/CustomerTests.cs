using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Test;

namespace OrderManagement.Domain.Tests
{
    public class CustomerTests : TestBase
    {
        [TestCase("customer1")]
        [TestCase("customer2")]
        public void Constructor_WithCustomerName_ShouldInitializeProperly(string customerName)
        {
            //Act 
            Customer customer = new Customer(customerName);

            //Assert
            Assert.NotNull(customer.Id);
            Assert.That(customer.Name, Is.EqualTo(customerName));
        }

        [TestCase("")]
        [TestCase(null)]
        public void Constructor_WithEmpltyName_ShouldInitializeNameAnoniem(string customerName)
        {
            //Act 
            Customer customer = new Customer(customerName);

            //Assert
            Assert.NotNull(customer.Id);
            Assert.That(customer.Name, Is.EqualTo("Anoniem"));
        }

        [TestCase("09a09c97-abbd-4793-80db-b7eb828c46fc","customer1")]
        [TestCase("d8a29271-2b42-44c8-aa0f-95c071563493","customer2")]
        public void Constructor_WithCustomerNameAndId_ShouldInitializeProperly(string id,string customerName)
        {
            //Act 
            Customer customer = new Customer(id, customerName);

            //Assert
            Assert.That(customer.Id.ToString(), Is.EqualTo(id));
            Assert.That(customer.Name, Is.EqualTo(customerName));
        }

        [Test]
        public void CreatNew_ShouldCreateCustomer()
        {

            //Assert
            Assert.That(Customer.CreateNew("09a09c97-abbd-4793-80db-b7eb828c46fc", "customer1").Id.ToString(), Is.EqualTo("09a09c97-abbd-4793-80db-b7eb828c46fc"));
            Assert.That(Customer.CreateNew("09a09c97-abbd-4793-80db-b7eb828c46fc", "customer1").Name, Is.EqualTo("customer1"));
        }

    }
}
