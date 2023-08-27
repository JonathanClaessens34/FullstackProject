using CommunityToolkit.Maui;
using IdentityModel.OidcClient;
using KWops.Mobile.Services.Backend;
using Order.Mobile.Services;
using Order.Mobile.Services.Backend;
using Order.Mobile.Services.Identity;
using Order.Mobile.Settings;
using Order.Mobile.ViewModels;
using Order.Mobile.Views;

namespace Order.Mobile;


public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        var builder = MauiApp.CreateBuilder();

        // Initialise the toolkit
        builder.UseMauiApp<App>().UseMauiCommunityToolkit();

        // the rest of your logic...
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        RegisterDependencies(builder.Services);

        return builder.Build();
    }

    private static void RegisterDependencies(IServiceCollection services)
    {
        // Register your dependencies here
        // Views
        services.AddTransient<LoginPage>();
        services.AddTransient<BarPage>();
        services.AddTransient<MenuPage>();
        services.AddTransient<OrderPage>();
        services.AddTransient<OrderHistoryPage>();

        // ViewModels
        services.AddTransient<LoginViewModel>();
        services.AddTransient<CocktailMenusViewModel>();
        services.AddTransient<MenuViewModel>();
        services.AddTransient<OrderViewModel>();
        services.AddTransient<OrderHistoryViewModel>();


        // Services
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<INavigationService, NavigationService>();
        services.AddTransient<IToastService, ToastService>();
        services.AddTransient<ICocktailMenuService, CocktailMenuService>();
        services.AddTransient<IBackendService, BackendService>();
        services.AddTransient<IOrderService, OrderService>();

        // Other
        services.AddSingleton<ITokenProvider, TokenProvider>();
        services.AddSingleton<IAppSettings, DevAppSettings>();


        //register detail route(s)
        Routing.RegisterRoute("MenuPage", typeof(MenuPage));
        Routing.RegisterRoute("OrderPage", typeof(OrderPage));

    }

}
