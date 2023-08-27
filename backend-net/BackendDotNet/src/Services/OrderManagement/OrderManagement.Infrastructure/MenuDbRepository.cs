using Domain;
using Microsoft.EntityFrameworkCore;
using OrderManagement.AppLogic;
using OrderManagement.Domain;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Infrastructure
{
    internal class MenuDbRepository : ICocktailMenuRepository
    {

        private readonly OrderManagementContext _context;

        public MenuDbRepository(OrderManagementContext devOpsContext)
        {
            _context = devOpsContext;
        }

        public async Task addAsync(CocktailMenu? menu)
        {
            await _context.Menus.AddAsync(menu);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(CocktailMenu? cocktailMenu)
        {
            _context.Menus.Remove(cocktailMenu);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CocktailMenu?>> GetAll()
        {
             return await _context.Menus.Include(item => item.Cocktails).ThenInclude(item => item.Cocktail).ToListAsync<CocktailMenu>();
            //return await _context.Menus.ToListAsync<CocktailMenu>();
        }

        public async Task<CocktailMenu?> GetByIdAsync(Guid id)
        {
            return await _context.Menus.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<CocktailMenu?> GetByStringIdAsync(string id)
        {
            return await _context.Menus.Where(x => x.Id.ToString() == id).FirstOrDefaultAsync();
        }

        public async Task<Cocktail> GetCocktailBySerialNumberAsync(Guid id, SerialNumber cocktailBarcode)
        {
            CocktailMenu menu = await _context.Menus.Where(x => x.Id == id).FirstOrDefaultAsync();
            return menu.FindBySerialnumber(cocktailBarcode);
        }

        public async Task saveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task saveChangesAsyncAlways(CocktailMenu cocktailMenu)
        {
            _context.Menus.Attach(cocktailMenu);
            _context.Entry(cocktailMenu).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            //context.SaveChanges();
            await _context.SaveChangesAsync();
        }

    }
}
