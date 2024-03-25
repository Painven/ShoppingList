using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            // внедряем сервисы
            .ConfigureServices(services =>
            {
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

