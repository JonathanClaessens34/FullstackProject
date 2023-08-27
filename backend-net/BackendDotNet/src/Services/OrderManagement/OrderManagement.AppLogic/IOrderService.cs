using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic
{
    public interface IOrderService
    {
        Task<Order> AddCocktailToOrderAsync(Guid orderId, Guid menuItemId);
        Task<Order> DeleteCocktailFromOrderAsync(Guid orderId, string cocktailSerialNumber, double price);
        Task DeleteOrderAsync(Guid orderId);
        Task<Order> FinelizeOrder(Guid orderId, int tableNr);
        Task<List<Order>> GetOrderHistory(Guid customerId);
        Task<Order> InitializeNewOrderAsync(string customerId, string barId);
    }
}
