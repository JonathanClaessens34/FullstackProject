using System.Diagnostics;
using IdentityModel.OidcClient.Browser;

namespace Order.Mobile.Services.Identity;

internal class WebAuthenticatorBrowser : IdentityModel.OidcClient.Browser.IBrowser
{
    public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
    {
        try
        {
            Uri startUrl = new Uri(options.StartUrl);
            Uri callbackUrl = new Uri(options.EndUrl);
            WebAuthenticatorResult authResult = await WebAuthenticator.AuthenticateAsync(startUrl, callbackUrl);
            string authorizeResponse = ToRawIdentityUrl(options.EndUrl, authResult);

            return new BrowserResult
            {
                Response = authorizeResponse
            };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return new BrowserResult()
            {
                ResultType = BrowserResultType.UnknownError,
                Error = ex.ToString()
            };
        }
    }

    public string ToRawIdentityUrl(string redirectUrl, WebAuthenticatorResult result)
    {
        IEnumerable<string> parameters = result.Properties.Select(pair => $"{pair.Key}={pair.Value}");
        var values = string.Join("&", parameters);

        return $"{redirectUrl}#{values}";
    }
}