using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Mobile.Services.Identity;

public interface ILoginResult
{
    string AccessToken { get; }
    string Error { get; }
    string ErrorDescription { get; }
    bool IsError { get; }

    string Sub { get; }
}