using Order.Mobile.Exceptions;
using Order.Mobile.Models;
using Order.Mobile.Services.Identity;
using Order.Mobile.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Mobile.Services.Backend;

public class OrderService : IOrderService
{

    private readonly IBackendService _backend;
    private readonly IAppSettings _appSettings;
    private readonly ITokenProvider _tokenProvider;
    private readonly INavigationService _navigationService;

    public OrderService(IBackendService backend,
        IAppSettings appSettings,
        INavigationService navigationService, ITokenProvider tokenProvider)
    {
        _backend = backend;
        _appSettings = appSettings;
        _navigationService = navigationService;
        _tokenProvider = tokenProvider;
    }

    public async Task<OrderClass> MakeNewOrderAsync(string barId) //hier guid aan toevoege wnr id server oke
    {

        try
        {
            string customerId = "none";
            if (_tokenProvider.Sub != null)
            {
                customerId = _tokenProvider.Sub;
            }
            //?customerId=  dit is goed zo mess als tijd in body steken
            OrderClass newOrder = await _backend.PostAsyncWithString<OrderClass>($"{_appSettings.OrderManagementBackendBaseUrl}/new?customerId={customerId}&barId={barId}", customerId); // /api/Order  http://localhost:5000/new  {_appSettings.OrderManagementBackendBaseUrl}/new

            string test = newOrder.Id.ToString();
            return newOrder; //mess naar barnaam veranderen
        }
        catch (BackendAuthenticationException)
        {
            await _navigationService.NavigateAsync("BarPage");
            return null; //moet dit wel?
        }


    }


    public async Task<OrderClass> AddToOrderAsync(Guid orderId, Guid menuItemId) //hier guid aan toevoege wnr id server oke
    {

        try
        {
            OrderClass updatedOrder = await _backend.PutAsyncWithString<OrderClass>($"{_appSettings.OrderManagementBackendBaseUrl}/api/order/{orderId.ToString()}?menuItemId={menuItemId.ToString()}", "none"); // /api/Order 
            return updatedOrder; //mess naar barnaam veranderen
        }
        catch (BackendAuthenticationException)
        {
            await _navigationService.NavigateAsync("BarPage");
            return null; //moet dit wel?
        }


    }

    public async Task<OrderClass> DeleteFromOrderAsync(Guid orderId, string serialNumber, double price) //hier guid aan toevoege wnr id server oke
    {

        try
        {
            //OrderClass updatedOrder = hier onder is zien wa doen om order te update mess me een update fzo en nie een dele ma da zien ik nog wel
            OrderClass updatedOrder =  await _backend.DeleteAsyncWithResult<OrderClass>($"{_appSettings.OrderManagementBackendBaseUrl}/api/order/{orderId.ToString()}/{serialNumber}?price={price.ToString()}"); // /api/Order 
            //return updatedOrder; //mess naar barnaam veranderen
            return updatedOrder;
        }
        catch (BackendAuthenticationException)
        {
            await _navigationService.NavigateAsync("BarPage");
            return null; //moet dit wel?
        }


    }

    public async Task<OrderClass> FinalizeOrderAsync(Guid orderId, int tableNr)
    {
        try
        {
            OrderClass newOrder = await _backend.PostAsyncWithString<OrderClass>($"{_appSettings.OrderManagementBackendBaseUrl}/pay?orderId={orderId.ToString()}&tableNr={tableNr.ToString()}", "none"); 
            await _navigationService.NavigateAsync("BarPage");
            return null;
        }
        catch (BackendAuthenticationException)
        {
            //await _navigationService.NavigateAsync("BarPage");
            return null; //moet dit wel?
        }
    }

    public async Task<IReadOnlyList<OrderClass>> GetOrderHistory()
    {
        try
        {
            string customerId = "none";
            if (_tokenProvider.Sub != null)
            {
                customerId = _tokenProvider.Sub;
            }
            List<OrderClass> orders = await _backend.GetAsync<List<OrderClass>>($"{_appSettings.OrderManagementBackendBaseUrl}/history/{customerId}");
            return orders.OrderBy(m => m.Id).ToList(); 
        }
        catch (BackendAuthenticationException)
        {
            await _navigationService.NavigateAsync("BarPage");
            return new List<OrderClass>();
        }
    }

}
