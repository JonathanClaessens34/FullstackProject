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
    internal class OrderItemDbRepository : IOrderItemRepository
    {

        private readonly OrderManagementContext _context;

        public OrderItemDbRepository(OrderManagementContext context)
        {
            _context = context;
        }

        public async Task<OrderItem> GetById(Guid orderItemId)
        {
            //return await _context.MenuItems.Where(x => x.Id == menuItemId).Include(item => item.Cocktail).FirstOrDefaultAsync();

            return await _context.OrderItems.Where(x => x.Id == orderItemId).FirstOrDefaultAsync();
        }

        public async Task addAsync(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task CommitTrackedChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
