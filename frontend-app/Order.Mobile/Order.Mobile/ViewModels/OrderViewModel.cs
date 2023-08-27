using IdentityModel.OidcClient;
using Order.Mobile.Models;
using Order.Mobile.Services;
using Order.Mobile.Services.Backend;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Order.Mobile.ViewModels;

//get order
public class OrderViewModel : BaseViewModel
{

    //niet alles wat hier staat moet in dees bestand persee
    //order moet aangemaakt worde bij aanmake van deze vieuw  (mess kijke of er nog ene ope stond)
    //Mogenlijkheid voor dingen toe te voegen en te verwijderen
    //order doorgeven naar backend


    private readonly IOrderService _orderService;
    private IToastService _toastService;

    private OrderClass _orderClass; //dees opslaan in vorig viewmodel?

    public ICommand FinalizeOrderCommand { get; } // veranderen naar order maybe
                                                  //mess remove order command

    private ObservableCollection<OrderItem> _orderItems;

    public ObservableCollection<OrderItem> OrderItems {
        get => _orderItems;
        set => SetProperty(ref _orderItems, value);
    } //heb dit in klasse zelf al gedaan dus maybe nie nodig

    //dees omvormen naar lijst met gekochte cocktails

    public ICommand DeleteFromOrderCommand => new Command<OrderItem>(ExecuteDeleteItemFromOrder); //{ get; }  dit zou moette werken

    public OrderClass OrderClass
    {
        get => _orderClass;
        set => SetProperty(ref _orderClass, value);
        
    }

    public OrderViewModel(IOrderService orderService , IToastService toastService)
    {
        _toastService = toastService;
        _orderService = orderService;
        FinalizeOrderCommand = new Command(async () => await ExecuteFinalizeOrder());
        OrderItems = new ObservableCollection<OrderItem>();

        MessagingCenter.Subscribe<MenuViewModel, OrderClass>(this, "OrderSelected", (MenuViewModel, openOrder) =>
        {
            //_orderClass = openOrder; //late weten da data is veranderd
            OrderClass = openOrder;

            if (openOrder.OrderItems != null)
            {
                IReadOnlyList<OrderItem> allOrderItems = openOrder.OrderItems;
                foreach (var orderItem in allOrderItems)
                {
                    OrderItems.Add(orderItem);
                }

            }

        });

        //_orderClass.totalPrice= 2.0;

    }
    internal async void ExecuteDeleteItemFromOrder(OrderItem item) //als test wnr nie werkt mess de andere metode
    {
        OrderClass = await _orderService.DeleteFromOrderAsync(_orderClass.Id, item.Cocktail.SerialNumber.Nummer, item.Price);
        OrderItems = new ObservableCollection<OrderItem>();
        if (OrderClass.OrderItems != null)
        {
            IReadOnlyList<OrderItem> allOrderItems = OrderClass.OrderItems;
            foreach (var orderItem in allOrderItems)
            {
                OrderItems.Add(orderItem);
            }

        }

    }

    internal async Task ExecuteFinalizeOrder() //als test wnr nie werkt mess de andere metode
    {
        if (OrderClass.Table != 0)
        {
            await _orderService.FinalizeOrderAsync(OrderClass.Id, OrderClass.Table);
        }
        else
        {
            await _toastService.DisplayToastAsync("TableNr cant be 0");
        }
        

    }
}
