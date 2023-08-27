using Order.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Mobile.Services.Backend
{
    public interface IOrderService
    {
        Task<OrderClass> MakeNewOrderAsync(string barId);
        Task<OrderClass> AddToOrderAsync(Guid orderId, Guid menuItemId);
        Task<OrderClass> DeleteFromOrderAsync(Guid orderId, string serialNumber, double price);
        Task<OrderClass> FinalizeOrderAsync(Guid orderId, int tableNr);
        Task<IReadOnlyList<OrderClass>> GetOrderHistory();
    }
}
