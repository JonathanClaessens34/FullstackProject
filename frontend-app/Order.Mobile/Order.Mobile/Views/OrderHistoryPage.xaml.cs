using Order.Mobile.ViewModels;

namespace Order.Mobile.Views;

public partial class OrderHistoryPage : ContentPage
{
	public OrderHistoryPage(OrderHistoryViewModel orderHistoryViewModel)
	{
		InitializeComponent();
		BindingContext = orderHistoryViewModel;
		
	}
}