using CommunityToolkit.Mvvm.ComponentModel;

namespace ShoppingList.Desktop.ViewModels;


public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private string watchingFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

    public MainWindowViewModel()
    {

    }
}
