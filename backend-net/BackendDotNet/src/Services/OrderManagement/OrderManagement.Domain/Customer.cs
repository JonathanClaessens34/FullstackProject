using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain
{
    public class Customer : Entity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } //Standaard Anoniem fzo make mess veranderen en gwn zien of nie ingeolgd of dit verplaatsen naar identitry fzo


        public Customer(string name)
        {
            Id = Guid.NewGuid();
            if (name == null || name == "")
            {
                Name = "Anoniem";
            }
            else { 
                Name = name;
            }
        }

        public Customer(string guid, string name)
        {
            Id = Guid.Parse(guid);
            if (name == null || name == "")
            {
                Name = "Anoniem";
            }
            else
            {
                Name = name;
            }
        }

        protected override IEnumerable<object> GetIdComponents()
        {
            yield return Id;
        }

        public static Customer CreateNew(string guid, string name)
        {
            return new Customer(guid, name);
        }
    }
}
