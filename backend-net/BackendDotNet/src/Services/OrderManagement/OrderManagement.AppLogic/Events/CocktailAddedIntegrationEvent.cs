using AppLogic.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic.Events
{
    public class CocktailAddedIntegrationEvent : IntegrationEvent
    {

        public string Name { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

    }
}
