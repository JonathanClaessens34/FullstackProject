using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Mobile.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public Cocktail Cocktail { get; set; }
        public double Price { get; set; }


        public OrderItem()
        {

        }
        public OrderItem(int amount, MenuItem menuItem)
        {
            Id= Guid.NewGuid();
            Amount = amount;
            Cocktail = menuItem.Cocktail;
            Price = menuItem.Price;
        }


    }
}
