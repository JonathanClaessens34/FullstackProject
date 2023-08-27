using Microsoft.AspNetCore.Mvc;
using OrderManagement.AppLogic;
using OrderManagement.Domain;
using System.ComponentModel.Design;

namespace OrderManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {

        //kleine controller met heel weinig endpoints maar voor propere opsplitsing (mess zelfs niet mogenlijk aan de hand van hoe mvc verloopt met maui)
        //voor get menu (en mess in pages op te delen ofzo), mess prijzen hier al aanpasssen of enkel in afrekening
        ICocktailMenuService _cocktailMenuService;
        ICocktailMenuRepository _cocktailMenuRepository;
        ICocktailRepository _cocktailRepository;
        IMenuItemRepository _menuItemRepository;

        public MenuController(ICocktailMenuRepository menuRepository, ICocktailMenuService menuService, ICocktailRepository cocktailRepository, IMenuItemRepository menuItemRepository)
        {
            _cocktailMenuRepository = menuRepository;
            _cocktailMenuService = menuService;
            _cocktailRepository = cocktailRepository;
            _menuItemRepository = menuItemRepository;
        }



        [HttpGet("menu/{id}")]
        public async Task<IActionResult> GetMenuByID(Guid id)
        {
            CocktailMenu? menu = await _cocktailMenuRepository.GetByIdAsync(id);
            return menu == null ? NotFound() : Ok(menu);
        }

        [HttpGet("menuitem/{menuitemid}")]
        public async Task<IActionResult> GetMenuitemByID(Guid id)
        {
            MenuItem? menu = await _menuItemRepository.GetById(id);
            return menu == null ? NotFound() : Ok(menu);
        }

        [HttpGet("seed")]
        public async Task<IActionResult> Seed()
        {
            //CocktailMenu? menu = await _cocktailMenuRepository.GetByIdAsync(id);
            CocktailMenu newMenu = new CocktailMenu("Name");
            Cocktail newCocktail = new Cocktail("5449000111654", "nee", "www.test.be");
            MenuItem newMenuItem = new MenuItem(newCocktail, 2.1);
            await _cocktailMenuRepository.addAsync(newMenu);
            await _cocktailRepository.AddAsync(newCocktail);
            await _menuItemRepository.addAsync(newMenuItem);
            newMenu.AddCocktail(newMenuItem);
            await _cocktailMenuRepository.saveChangesAsync();




            return Ok();
        }


        //get all
        [HttpGet("getAllCocktails")]
        public async Task<IActionResult> GetAllCocktails()
        {
            List<Cocktail?> menus = await _cocktailRepository.GetAll();
            return menus == null ? NotFound() : Ok(menus);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllMenus()
        {
            List<CocktailMenu?> menus = await _cocktailMenuRepository.GetAll();

            //List<CocktailMenu> menus = new List<CocktailMenu>();
            //
            //CocktailMenu newMenu = new CocktailMenu("KWoptails");
            //CocktailMenu newNewMenu = new CocktailMenu("Super Bar");
            //CocktailMenu newNewNewMenu = new CocktailMenu("SummerCocktails");
            //CocktailMenu newNewNewNewMenu = new CocktailMenu("La Margarita");
            //
            //Cocktail specialCocktail = new Cocktail("8710400311140", "Kwocktail", 8.65);
            //Cocktail specialCocktail2 = new Cocktail("8710400311140", "Margarita", 9.62);
            //Cocktail specialCocktail3 = new Cocktail("8710400311140", "FcDeKampioenen Cocktail", 21.04);
            //Cocktail specialCocktail4 = new Cocktail("8710400311140", "Passoa Special", 6.25);
            //
            //newMenu.AddCocktail(specialCocktail);
            //newNewMenu.AddCocktail(specialCocktail);
            //newNewNewMenu.AddCocktail(specialCocktail);
            //newNewNewNewMenu.AddCocktail(specialCocktail);
            //newMenu.AddCocktail(specialCocktail2);
            //newNewMenu.AddCocktail(specialCocktail2);
            //newNewNewMenu.AddCocktail(specialCocktail2);
            //newNewNewNewMenu.AddCocktail(specialCocktail2);
            //newMenu.AddCocktail(specialCocktail3);
            //newNewMenu.AddCocktail(specialCocktail3);
            //newNewNewMenu.AddCocktail(specialCocktail3);
            //newNewNewNewMenu.AddCocktail(specialCocktail3);
            //newMenu.AddCocktail(specialCocktail4);
            //newNewMenu.AddCocktail(specialCocktail4);
            //newNewNewMenu.AddCocktail(specialCocktail4);
            //newNewNewNewMenu.AddCocktail(specialCocktail4);
            //
            //
            //menus.Add(newMenu);
            //menus.Add(newNewMenu);
            //menus.Add(newNewNewMenu);
            //menus.Add(newNewNewNewMenu);


            return menus == null ? NotFound() : Ok(menus);
        }

    }
}
