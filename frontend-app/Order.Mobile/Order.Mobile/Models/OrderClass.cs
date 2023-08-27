using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Mobile.Models
{
    public class OrderClass  //her noemen naar order
    {
        public Guid Id { get; set; }
        public Guid barId { get; set; }

        public ObservableCollection<OrderItem> OrderItems { get; set; } 
        public int Table { get; set; }
        public Guid CustomerId { get; set; }

        public double TotalPrice { get; set; }
        public bool Payed { get; set; }



    }
}
