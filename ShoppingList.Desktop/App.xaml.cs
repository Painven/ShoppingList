using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShoppingList.Core;
using ShoppingList.Core.Interfaces;
using ShoppingList.Desktop.BL;
using ShoppingList.Desktop.ViewModels;
using System.Windows;

namespace ShoppingList.Desktop;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost host;

    public App()
    {
        host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.Configure<ApiEndpointConfiguration>(context.Configuration.GetSection(nameof(ApiEndpointConfiguration)));
                services.AddHttpClient<ShoppingListApiClient>();
                services.AddSingleton<IShopListFileParser, ShopListTxtFileParser>();
                services.AddSingleton<MainWindowViewModel>();
                services.AddSingleton<MainWindow>(x => new MainWindow()
                {
                    DataContext = x.GetRequiredService<MainWindowViewModel>()
                });
            })
            .Build();
    }
    protected override void OnStartup(StartupEventArgs e)
    {
        MainWindow = host.Services.GetRequiredService<MainWindow>();
        MainWindow.Show();

        base.OnStartup(e);

    }
}

