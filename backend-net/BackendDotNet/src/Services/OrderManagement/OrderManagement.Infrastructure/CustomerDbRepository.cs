using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrderManagement.AppLogic;
using System.Threading.Tasks;
using OrderManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace OrderManagement.Infrastructure
{
    internal class CustomerDbRepository : ICustomerRepository
    {
        private readonly OrderManagementContext _context;
        public CustomerDbRepository(OrderManagementContext devOpsContext)
        {
            _context = devOpsContext;
        }

        public async Task AddAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> GetByStringIdAsync(string customerId)
        {
            return await _context.Customers.Where(x => x.Id.ToString() == customerId).FirstOrDefaultAsync();
        }
    }
}
