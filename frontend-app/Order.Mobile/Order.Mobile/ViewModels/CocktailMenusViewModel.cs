using Order.Mobile.Models;
using Order.Mobile.Services;
using Order.Mobile.Services.Backend;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Mobile.ViewModels;

public class CocktailMenusViewModel : BaseViewModel
{
    private readonly ICocktailMenuService _cocktailMenuService;
    private readonly IToastService _toastService;
    private readonly INavigationService _navigationService;

    public ObservableCollection<CocktailMenu> Items { get; }
    public Command LoadMenusCommand { get; }
    public Command<CocktailMenu> ItemTapped { get; }

    public CocktailMenusViewModel(ICocktailMenuService cocktailMenuService,
        IToastService toastService,
        INavigationService navigationService)
    {
        _cocktailMenuService = cocktailMenuService;
        _toastService = toastService;
        _navigationService = navigationService;
        Title = "Bars Overview";
        Items = new ObservableCollection<CocktailMenu>();
        LoadMenusCommand = new Command(async () => await ExecuteLoadCocktailMenusCommand());

        ItemTapped = new Command<CocktailMenu>(OnCocktailMenuSelected);
    }

    internal async Task ExecuteLoadCocktailMenusCommand()
    {
        IsBusy = true;

        try
        {
            Items.Clear();
            IReadOnlyList<CocktailMenu> allCocktailMenus = await _cocktailMenuService.GetAllMenusAsync();
            foreach (var menu in allCocktailMenus)
            {
                Items.Add(menu);
            }
        }
        catch (Exception ex)
        {
            await _toastService.DisplayToastAsync(ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }

    internal async void OnCocktailMenuSelected(CocktailMenu menu)
    {
        if (menu == null)
        {
            return;
        }

        await _navigationService.NavigateRelativeAsync("MenuPage");
        MessagingCenter.Send(this, "MenuSelected", menu);
    }
}
