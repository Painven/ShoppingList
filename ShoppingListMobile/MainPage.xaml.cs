using ShoppingListMobile.ViewModels;

namespace ShoppingListMobile;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

}

