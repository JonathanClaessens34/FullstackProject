using AppLogic.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic.Events
{
    public class MenuAddedIntegrationEvent : IntegrationEvent
    {
        public string menuId { get; set; } = string.Empty;
        public string BarName { get; set; } = string.Empty;

    }
}
