using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain
{
    //mess terug naar internal ma is nie voor nu
    public class CocktailMenu : Entity
    {
        //ook zo handelen of gwn guid?
        public Guid Id { get; private set; }// id van de bar
        public string BarName { get; set; }

        private List<MenuItem> _cocktailList; //lijst van menu items aanmaken (cocktail en prijs)

        public List<MenuItem> Cocktails { get { return _cocktailList; } } // virtual maken als volgend idee

        public CocktailMenu() //idk of dit corrext is maar dit laat me migreren
        {
            Id = new Guid();
            BarName = string.Empty;
            _cocktailList = new List<MenuItem>();
        }
        public CocktailMenu( string barName)
        {
            Id = new Guid(); //tijdenlijk hopenlijk dit snel weg
            BarName = barName;
            _cocktailList = new List<MenuItem>();
        }
        public CocktailMenu(string id, string barId)
        {
            try
            {
                Id = new Guid(id);
            }
            catch (Exception error)
            {
                throw new Exception("conversion to guid problem: " + error);
            }
            BarName = barId;
            _cocktailList = new List<MenuItem>();
        }

        protected override IEnumerable<object> GetIdComponents()
        {
            yield return Id;
        }

        public void AddCocktail(MenuItem menuItem)
        {
            //List<MenuItem> newList = new List<MenuItem>();
            //newList = _cocktailList;
            //MenuItem newMenuItem = new MenuItem(cocktail, price);
            //newList.Add(newMenuItem);
            //_cocktailList = newList;
            //_cocktailList.Add(newMenuItem);
            Cocktails.Add(menuItem);
        }

        public void DeleteCocktail(string serialNumber)
        {

            MenuItem? _menuItem = null;
            foreach (var menuItem in _cocktailList)
            {
                if (menuItem.Cocktail.SerialNumber.ToString() == serialNumber)
                {
                    _cocktailList.Remove(_menuItem);
                }
            }
            

            
        }

        public Cocktail FindBySerialnumber(SerialNumber cocktailBarcode)
        {
            MenuItem? _menuItem = null;
            foreach (var menuItem in _cocktailList)
            {
                if(menuItem.Cocktail.SerialNumber == cocktailBarcode)
                {
                    _menuItem = menuItem;
                }
            }
            if (_menuItem != null)
            {
                return _menuItem.Cocktail;
            }
            else {
                throw new Exception("cocktail not found.");
            }
        }

        public static CocktailMenu CreateNew(string menuId, string barId)
        {

            return new CocktailMenu(menuId, barId);
        }
    }
}
