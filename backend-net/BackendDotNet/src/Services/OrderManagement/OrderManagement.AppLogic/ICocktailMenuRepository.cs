using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OrderManagement.AppLogic
{
    public interface ICocktailMenuRepository
    {
        Task<CocktailMenu?> GetByIdAsync(Guid id);
        Task<CocktailMenu?> GetByStringIdAsync(String id);
        Task<List<CocktailMenu?>> GetAll();
        Task<Cocktail> GetCocktailBySerialNumberAsync(Guid id, SerialNumber cocktailBarcode); //dees naar cocktail repository verplaatsen
        Task addAsync(CocktailMenu? menu);
        Task saveChangesAsync();
        Task saveChangesAsyncAlways(CocktailMenu cocktailMenu);
        Task DeleteAsync(CocktailMenu? cocktailMenu);
    }
}
