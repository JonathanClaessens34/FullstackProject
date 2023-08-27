using AppLogic.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic.Events
{
    internal class CocktailDeletedIntegrationEvent : IntegrationEvent
    {
        public string SerialNumber { get; set; } = string.Empty;
    }
}
