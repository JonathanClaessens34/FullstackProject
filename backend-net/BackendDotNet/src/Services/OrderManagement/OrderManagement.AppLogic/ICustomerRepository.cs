using Microsoft.Extensions.Logging;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic
{
    public interface ICustomerRepository
    {

        Task AddAsync(Customer customer);
        Task<Customer> GetByStringIdAsync(string customerId);

    }
}
