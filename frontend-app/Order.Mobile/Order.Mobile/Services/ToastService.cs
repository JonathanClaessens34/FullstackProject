using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace Order.Mobile.Services;

public class ToastService : IToastService
{
    public Task DisplayToastAsync(string message)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        string text = message;
        ToastDuration duration = ToastDuration.Long;
        double fontSize = 14;

        var toast = Toast.Make(text, duration, fontSize);

        return toast.Show(cancellationTokenSource.Token);
    }
}