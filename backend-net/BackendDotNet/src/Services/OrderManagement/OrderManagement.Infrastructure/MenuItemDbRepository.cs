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
    internal class MenuItemDbRepository : IMenuItemRepository
    {
        private readonly OrderManagementContext _context;

        public MenuItemDbRepository(OrderManagementContext context)
        {
            _context = context;
        }

        public async Task<MenuItem> GetById(Guid menuItemId)
        {
            return await _context.MenuItems.Where(x => x.Id == menuItemId).Include(item => item.Cocktail).FirstOrDefaultAsync();
            
            //return await _context.MenuItems.Where(x => x.Id == menuItemId).FirstOrDefaultAsync();
        }

        public async Task addAsync(MenuItem menuItem)
        {
            await _context.MenuItems.AddAsync(menuItem);
            await _context.SaveChangesAsync();
        }
    }
}
