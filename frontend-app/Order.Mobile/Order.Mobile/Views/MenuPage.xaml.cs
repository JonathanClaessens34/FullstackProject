using Order.Mobile.ViewModels;

namespace Order.Mobile.Views;

public partial class MenuPage : ContentPage
{
    public MenuPage(MenuViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

}