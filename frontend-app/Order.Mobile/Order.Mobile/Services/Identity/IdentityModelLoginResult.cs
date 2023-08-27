using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.OidcClient;

namespace Order.Mobile.Services.Identity;

public class IdentityModelLoginResult : ILoginResult
{
    private readonly LoginResult _result;

    public IdentityModelLoginResult(LoginResult result)
    {
        _result = result;
    }

    public IdentityModelLoginResult(string error, string errorDescription)
    {
        _result = new LoginResult(error, errorDescription);
    }

    public bool IsError => _result.IsError;
    public string AccessToken => _result.AccessToken;
    public string Error => _result.Error;
    public string ErrorDescription => _result.ErrorDescription;
    public string Sub
    {
        get
        {
            System.Security.Claims.ClaimsIdentity identity = _result.User.Identity as System.Security.Claims.ClaimsIdentity;
            return identity.Claims.ToList().ElementAt(1).Value;
        }
    

        } // _result.User.Identity.Claims[1].value;
    
}