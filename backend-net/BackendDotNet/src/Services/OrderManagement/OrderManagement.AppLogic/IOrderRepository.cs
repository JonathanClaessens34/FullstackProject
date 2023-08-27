using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic
{
    public interface IOrderRepository
    {
        Task AddAsync(Order newOrder);
        Task CommitTrackedChangesAsync();
        Task<Order> GetById(Guid orderId);
        Task DeleteOrder(Order order);

        Task<List<Order>> getOrderHistory(Guid customerId);
    }
}
