using Order.Mobile.Models;
using Order.Mobile.Services;
using Order.Mobile.Services.Backend;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Order.Mobile.ViewModels;

public class MenuViewModel : BaseViewModel
{
    //best hierin order class e bijhouden denk ik
    internal CocktailMenu _cocktailMenu;
    private readonly INavigationService _navigationService;

    /// ------------------------------
    private readonly IOrderService _orderService;
    internal OrderClass _orderClass; //wordt6 hier aangemaakt zodat er kan aan worden toegevoegd de order view model moet deze meegegeve krijge of tenminste dit id
    public ObservableCollection<OrderItem> OrderItems { get; }
    public OrderClass OrderClass
    {
        get => _orderClass;
        set => SetProperty(ref _orderClass, value);
    }

    /// ------------------------------

    public ObservableCollection<Models.MenuItem> Cocktails { get; }

    public ICommand ViewOrderCommand { get; }

    public ICommand AddToOrderCommand => new Command<Models.MenuItem>(ExecuteAddItemToOrder); //{ get; }  dit zou moette werken
    public CocktailMenu CocktailMenu
    {
        get => _cocktailMenu;
        set => SetProperty(ref _cocktailMenu, value);
    }

    public MenuViewModel(INavigationService navigationService, IOrderService orderService)
    {
        _orderService = orderService;
        OrderItems = new ObservableCollection<OrderItem>();
        _navigationService = navigationService;
        ViewOrderCommand = new Command(ExecuteLoadOrderCommand); //new Command(async () => await ExecuteLoadOrderCommand());//new Command(ExecuteLoadOrderCommand);
        //AddToOrderCommand = new Command(async () => await ExecuteAddItemToOrder());
        Cocktails = new ObservableCollection<Models.MenuItem>();
        MessagingCenter.Subscribe<CocktailMenusViewModel, CocktailMenu>(this, "MenuSelected", (CocktailMenusViewModel, selectedMenu) =>
        {
            CocktailMenu = selectedMenu;
            StartAsyncOperation();
            //zorgt voor crash wnr geen cocktails in menu

            if (selectedMenu.cocktails != null) {
                IReadOnlyList<Models.MenuItem> allCocktails = selectedMenu.cocktails;
                foreach (var cocktail in allCocktails)
                {
                    Cocktails.Add(cocktail);
                }
            }
        });

        // _orderClass = await _orderService.MakeNewOrderAsync();
        // _orderClass.totalPrice = 0.0;
        // Console.WriteLine(_orderClass);
        


    }


    internal async void ExecuteLoadOrderCommand()
    {

        if (_orderClass == null)
        {
            return;
        }
               
        await _navigationService.NavigateRelativeAsync("OrderPage");
        MessagingCenter.Send(this, "OrderSelected", OrderClass);

    }

    internal async void ExecuteAddItemToOrder(Models.MenuItem item) 
    {
        string test = item.Id.ToString();
        OrderClass = await _orderService.AddToOrderAsync(_orderClass.Id, item.Id);
        Console.WriteLine("test");
        
    }

    //dit kan maybe veranderd worden naar on appering via de xaml.cs, dit kan maybe verwijderd worden
    internal async void OnAppearing() 
    {
        if (_orderClass == null)
        {
            _orderClass = await _orderService.MakeNewOrderAsync(_cocktailMenu.id.ToString());
            _orderClass.TotalPrice = 0.0;
        }
        Console.WriteLine(_orderClass);
    }


    internal async void StartAsyncOperation()
    {
        
        _orderClass = await _orderService.MakeNewOrderAsync(_cocktailMenu.id.ToString());
        _orderClass.TotalPrice = 0.1;
        Console.WriteLine(_orderClass.TotalPrice);
    }


}
