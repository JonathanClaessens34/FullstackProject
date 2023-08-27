using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain
{
    public class Order : Entity
    {
        //moet er een datum bijgehoude worden?
        public Guid Id { get; private set; }

        public Guid BarId { get; set; }

        private List<OrderItem> _orderItemList;//zien da de prijs hetzelfde blijft als origineel
        public List<OrderItem> OrderItems { get { return _orderItemList; } }
        public int Table { get; set; }//tafel nr
        public Guid? CustomerId { get; set; }//als customer niet geset is discount geve, via apparte klasse of variabele hier in of gwn deze var checken?

        public double TotalPrice { get; set; }
        public bool Payed { get; set; }

        public Order(string barId)
        {
            Payed = false;
            Id = Guid.NewGuid();
            BarId = new Guid(barId);
            _orderItemList = new List<OrderItem>();
            TotalPrice = 0;
            CustomerId = null;
        }

        public Order() //werkt nie zonder dit idk why
        {
            Payed = false;
            Id = Guid.NewGuid();
            _orderItemList = new List<OrderItem>();
            TotalPrice = 0;
            CustomerId = null;
        }

        public Order(string customerId, string barId)
        {
            if (customerId != null && customerId != "") {
                CustomerId = new Guid(customerId);
            }
            BarId = new Guid(barId);
            Id = Guid.NewGuid();
            _orderItemList = new List<OrderItem>();
            TotalPrice= 0;
        }
        protected override IEnumerable<object> GetIdComponents()
        {
            yield return Id;
        }

        public void AddCocktail(OrderItem orderItem)
        {
            
            OrderItems.Add(orderItem);

        }

        public void DeleteCocktail(string serialNumber)  //doe nu -1 maar kan ook gwn verwijderen alsk nodig maar moet da make dan (geen prioriteit)
        {
            foreach (OrderItem item in OrderItems)
            {
                if (item.Cocktail.SerialNumber.ToString() == serialNumber)
                {
                    item.Amount -= 1;
                    if(item.Amount<= 0)
                    {
                        OrderItems.Remove(item);
                    }
                    break;
                }
            }
        }
    }
}
