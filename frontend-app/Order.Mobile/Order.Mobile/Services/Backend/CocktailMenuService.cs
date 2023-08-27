using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Order.Mobile.Exceptions;
using Order.Mobile.Models;
using Order.Mobile.Services;
using Order.Mobile.Services.Backend;
using Order.Mobile.Services.Identity;
using Order.Mobile.Settings;

namespace KWops.Mobile.Services.Backend;

public class CocktailMenuService : ICocktailMenuService
{
    private readonly IBackendService _backend;
    private readonly IAppSettings _appSettings;
    private readonly ITokenProvider _tokenProvider;
    private readonly INavigationService _navigationService;

    public CocktailMenuService(IBackendService backend,
        IAppSettings appSettings,
        ITokenProvider tokenProvider,
        INavigationService navigationService)
    {
        _backend = backend;
        _appSettings = appSettings;
        _tokenProvider = tokenProvider;
        _navigationService = navigationService;
    }

    public async Task<IReadOnlyList<CocktailMenu>> GetAllMenusAsync()
    {
        try
        {
            List<CocktailMenu> cocktailMenus = await _backend.GetAsync<List<CocktailMenu>>($"{_appSettings.OrderManagementBackendBaseUrl}/api/Menu/getAll");
            return cocktailMenus.OrderBy(m => m.id).ToList(); //mess naar barnaam veranderen
        }
        catch (BackendAuthenticationException)
        {
            await _navigationService.NavigateAsync("BarPage");
            return new List<CocktailMenu>();
        }
    }

}
