using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic
{
    public interface IMenuItemRepository
    {
        Task<MenuItem> GetById(Guid menuItemId);
        Task addAsync(MenuItem menu);
    }
}
