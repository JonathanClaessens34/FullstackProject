using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic
{
    public interface IOrderItemRepository
    {
        Task CommitTrackedChangesAsync();
        Task<OrderItem> GetById(Guid orderItemId);
        Task addAsync(OrderItem orderItem);
    }
}
