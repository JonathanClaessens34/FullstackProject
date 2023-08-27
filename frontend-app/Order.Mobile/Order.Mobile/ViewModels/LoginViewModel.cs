using Order.Mobile.Services;
using Order.Mobile.Services.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Order.Mobile.ViewModels;

public class LoginViewModel : BaseViewModel
{

    private IIdentityService _identityService;
    private ITokenProvider _tokenProvider;
    private INavigationService _navigationService;
    private IToastService _toastService;

    public Command LoginCommand { get; }

    public LoginViewModel(IIdentityService identityService, ITokenProvider tokenProvider, INavigationService navigationService, IToastService toastService)
    {
        _identityService = identityService;
        _tokenProvider = tokenProvider;
        _navigationService = navigationService;
        _toastService = toastService;

        LoginCommand = new Command(async () => await ExcecuteLoginCommand());

    }

    async Task ExcecuteLoginCommand()
    {

        if (IsBusy == false)
        {
            ILoginResult result;
            IsBusy = true;



            try
            {
                result = await _identityService.LoginAsync();
                if (result.IsError == false)
                {
                    _tokenProvider.AuthAccessToken = result.AccessToken;
                    _tokenProvider.Sub = result.Sub;
                    await _navigationService.NavigateAsync("BarPage");
                }
                else
                {
                    await _toastService.DisplayToastAsync(result.ErrorDescription);
                }

            }
            catch (Exception ex)
            {
                await _toastService.DisplayToastAsync(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

    }



}