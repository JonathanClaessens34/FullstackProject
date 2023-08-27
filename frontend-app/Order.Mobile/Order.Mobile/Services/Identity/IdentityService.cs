using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Threading.Tasks;
using IdentityModel.OidcClient;
using Order.Mobile.Settings;
using Order.Mobile.Services.Identity;

namespace Order.Mobile.Services.Identity;

public class IdentityService : IIdentityService
{
    private readonly IAppSettings _appSettings;

    public IdentityService(IAppSettings appSettings)
    {
        _appSettings = appSettings;
    }

    public async Task<ILoginResult> LoginAsync()
    {
        var options = new OidcClientOptions
        {
            Authority = _appSettings.OidcAuthority,
            ClientId = _appSettings.OidcClientId,
            ClientSecret = _appSettings.OidcClientSecret,
            Scope = _appSettings.OidcScope,
            RedirectUri = _appSettings.OidcRedirectUri,
            Browser = new WebAuthenticatorBrowser()
        };

       // var options = new OidcClientOptions
       // {
       //     Authority = "http://10.0.2.2:9000",
       //     ClientId = "order.mobile",
       //     ClientSecret = "MobileClientSecret",
       //     Scope = "openid manage",
       //     RedirectUri = "myapp://mauicallback",
       //     Browser = new WebAuthenticatorBrowser()
       // };

        var client = new OidcClient(options);
        #if DEBUG
        client.Options.Policy.Discovery.RequireHttps = false;
        #endif
        try
        {
            LoginResult result = await client.LoginAsync(new LoginRequest());  //client zenne claim me de naam sub is het id (Subject id)
            return new IdentityModelLoginResult(result); // dees aanpassen voor meer info terug te geven?

        }
        catch (Exception e)
        {
            return new IdentityModelLoginResult("Unexpected error", e.Message);
        }
    }
}