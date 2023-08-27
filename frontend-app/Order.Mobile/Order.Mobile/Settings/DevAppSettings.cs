using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Mobile.Settings;

public class DevAppSettings : IAppSettings
{
    public string OidcAuthority => "http://10.0.2.2:9000";
    public string OidcClientId => "order.mobile";
    public string OidcClientSecret => "MobileClientSecret";
    public string OidcScope => "openid order.read"; 
    public string OidcRedirectUri => "myapp://mauicallback";

    public string OrderManagementBackendBaseUrl => "http://10.0.2.2:5000"; 
}