using Microsoft.EntityFrameworkCore;
using OrderManagement.AppLogic;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Infrastructure
{
    internal class OrderDbRepository : IOrderRepository
    {
        private readonly OrderManagementContext _context;

        public OrderDbRepository(OrderManagementContext devOpsContext)
        {
            _context = devOpsContext;
        }

        public async Task AddAsync(Order newOrder)
        {
            await _context.AddAsync(newOrder);
            await _context.SaveChangesAsync();
        }

        public async Task CommitTrackedChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetById(Guid orderId)
        {
            return await _context.Orders.Where(x => x.Id == orderId).Include(item => item.OrderItems).ThenInclude(item => item.Cocktail).FirstOrDefaultAsync();
        }

        public async Task<List<Order>> getOrderHistory(Guid customerId)
        {
            return  await _context.Orders.Where(x => x.CustomerId == customerId).Include(item => item.OrderItems).ThenInclude(item => item.Cocktail).ToListAsync();
            
        }
    }
}
