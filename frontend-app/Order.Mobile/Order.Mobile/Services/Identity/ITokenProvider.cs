using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Mobile.Services.Identity;

public interface ITokenProvider
{
    string AuthAccessToken { get; set; }
    string Sub { get; set; }
}