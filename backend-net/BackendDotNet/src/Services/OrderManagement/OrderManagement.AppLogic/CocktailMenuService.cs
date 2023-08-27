using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic
{
    public class CocktailMenuService : ICocktailMenuService
    {
        public ICocktailMenuRepository MenuRepository;

        public CocktailMenuService(ICocktailMenuRepository menuRepositroy) { 
        
            MenuRepository = menuRepositroy;
        }
    }
}
