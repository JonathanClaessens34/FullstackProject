using AppLogic.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic.Events
{
    public class CocktailAddedToMenuIntegrationEvent : IntegrationEvent
    {

        public string menuId { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;

        public string Price { get; set; } = string.Empty;
    }
}
