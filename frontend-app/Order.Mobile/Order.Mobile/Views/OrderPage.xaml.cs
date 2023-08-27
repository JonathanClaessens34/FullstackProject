using Order.Mobile.ViewModels;

namespace Order.Mobile.Views;

public partial class OrderPage : ContentPage
{
    //private OrderViewModel _viewModel; dees weetk nog nie
    public OrderPage(OrderViewModel orderViewModel)
    {
        InitializeComponent();
        BindingContext = orderViewModel;
        //_viewModel = viewModel; dees weetk nog nie

    }

    //protected override void OnAppearing() hem ik nodig denk ik maar nog eve wachten
    //{
    //    base.OnAppearing();
    //    _viewModel.OnAppearing();
    //
    //}
}