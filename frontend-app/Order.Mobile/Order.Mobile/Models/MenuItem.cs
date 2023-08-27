using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Mobile.Models
{
    public class MenuItem //is dit een entity?? nee denk ik toch??
    {
        public Guid Id { get; set; }
        public Cocktail Cocktail { get; set; }
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

    }
}
