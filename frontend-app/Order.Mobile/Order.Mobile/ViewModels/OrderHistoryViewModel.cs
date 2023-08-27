using Order.Mobile.Models;
using Order.Mobile.Services.Backend;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Mobile.ViewModels
{
    public class OrderHistoryViewModel : BaseViewModel
    {

        public ObservableCollection<OrderClass> Orders { get; }

        private readonly IOrderService _orderService;

        public Command LoadOrderHistoryCommand { get; }



        public OrderHistoryViewModel(IOrderService orderService)
        {
            _orderService= orderService;
            Orders = new ObservableCollection<OrderClass>();
            LoadOrderHistoryCommand = new Command(async () => await ExecuteLoadOrderHistoryCommand());

        }


        internal async Task ExecuteLoadOrderHistoryCommand()
        {

            IsBusy = true;

            try
            {
                Orders.Clear(); 
                IReadOnlyList<OrderClass> allOrders = await _orderService.GetOrderHistory();
                foreach (var menu in allOrders)
                {
                    Orders.Add(menu);
                }
            }
            catch (Exception ex)
            {
                //await _toastService.DisplayToastAsync(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }


    }
}
