using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Mobile.Services.Identity;

public class TokenProvider : ITokenProvider
{
    private const string AccessToken = "access_token";

    public string AuthAccessToken
    {
        get => Preferences.Get(AccessToken, string.Empty);
        set => Preferences.Set(AccessToken, value);
    }


    public string Sub { get; set; }
}