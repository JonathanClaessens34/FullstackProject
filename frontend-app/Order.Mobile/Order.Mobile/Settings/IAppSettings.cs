using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Mobile.Settings;

public interface IAppSettings
{
    string OidcAuthority { get; }
    string OidcClientId { get; }
    string OidcClientSecret { get; }
    string OidcScope { get; }
    string OidcRedirectUri { get; }

    string OrderManagementBackendBaseUrl { get; }
}