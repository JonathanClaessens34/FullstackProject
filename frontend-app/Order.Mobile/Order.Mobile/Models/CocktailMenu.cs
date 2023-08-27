using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Mobile.Models
{
    //mess terug naar internal ma is nie voor nu
    public class CocktailMenu { 

        //ook zo handelen of gwn guid?
        public Guid id { get; set; }
        public string barName { get; set; }

        public List<MenuItem> cocktails { get; set; }



    }
}
