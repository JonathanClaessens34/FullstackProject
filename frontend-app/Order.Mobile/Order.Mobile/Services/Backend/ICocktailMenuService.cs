using Order.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Mobile.Services.Backend
{
    public interface ICocktailMenuService
    {

        Task<IReadOnlyList<CocktailMenu>> GetAllMenusAsync();

    }
}
