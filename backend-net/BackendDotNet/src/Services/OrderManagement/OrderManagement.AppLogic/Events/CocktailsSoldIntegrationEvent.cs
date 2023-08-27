using AppLogic.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic.Events
{
    internal class CocktailsSoldIntegrationEvent :IntegrationEvent
    {

        public string BarId { get; set; } = string.Empty;
        public string CocktailBarcode { get; set; } = string.Empty;
        public string Aantal { get; set; } = string.Empty;
    }
}
