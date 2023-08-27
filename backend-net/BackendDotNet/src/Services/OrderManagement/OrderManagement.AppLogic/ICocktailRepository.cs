using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic
{
    public interface ICocktailRepository
    {
        Task<List<Cocktail?>> GetAll();
        Task AddAsync(Cocktail cocktail);
        Task<Cocktail> GetBySerialNumberAsync(string serialNumber);
        Task<Cocktail> GetCocktailBySerialNumberAsync(SerialNumber cocktailBarcode);
        Task CommitTrackedChangesAsync();
        Task DeleteAsync(Cocktail cocktail);
    }
}
