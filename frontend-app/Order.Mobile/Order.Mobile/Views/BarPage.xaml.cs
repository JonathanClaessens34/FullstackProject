using Order.Mobile.ViewModels;

namespace Order.Mobile.Views;

public partial class BarPage : ContentPage
{

    private CocktailMenusViewModel _viewModel;
    public BarPage(CocktailMenusViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }
    


}
