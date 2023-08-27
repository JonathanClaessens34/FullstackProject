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
    internal class CocktailDbRepository : ICocktailRepository
    {
        private readonly OrderManagementContext _context;

        public CocktailDbRepository(OrderManagementContext devOpsContext)
        {
            _context = devOpsContext;
        }

        public async Task AddAsync(Cocktail cocktail)
        {
            _context.Cocktails.Add(cocktail);
            await _context.SaveChangesAsync();
        }

        public async Task CommitTrackedChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Cocktail cocktail)
        {
            _context.Cocktails.Remove(cocktail);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Cocktail?>> GetAll()
        {
            return await _context.Cocktails.ToListAsync<Cocktail>();
        }

        public async Task<Cocktail> GetBySerialNumberAsync(string serialNumber)
        {
            SerialNumber hulp;
            try
            {
                hulp = new SerialNumber(serialNumber);
            }catch(Exception ex) {
                throw new Exception(ex.ToString());
            }
            Cocktail cocktail = await _context.Cocktails.Where(x => x.SerialNumber == hulp).FirstOrDefaultAsync();
            return cocktail;
        }

        public async Task<Cocktail> GetCocktailBySerialNumberAsync(SerialNumber cocktailBarcode)
        {
            Cocktail cocktail = await _context.Cocktails.Where(x => x.SerialNumber == cocktailBarcode).FirstOrDefaultAsync();
            return cocktail;
        }

        


    }
}
