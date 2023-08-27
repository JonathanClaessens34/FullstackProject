using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain
{
    public class MenuItem : Entity
    {
        public Guid Id { get; private set; }
        public Cocktail Cocktail { get; set; } //mess virtual
        public double Price { get; set; }

        public MenuItem()
        {

        }
        public MenuItem(Cocktail cocktail, double price)
        {
            Id =  Guid.NewGuid();
            Cocktail= cocktail;
            Price= price;
        }

        protected override IEnumerable<object> GetIdComponents()
        {
            yield return Id;
        }
    }
}
