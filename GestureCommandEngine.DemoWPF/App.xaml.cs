using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace GestureCommandEngine.DemoWPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private ServiceProvider _serviceProvider;
    public ServiceProvider ServiceProvider => _serviceProvider;

    public App()
    {
        ServiceCollection services = new ServiceCollection();

        ServicesConfiguration.ConfigureServices(services);

        _serviceProvider = services.BuildServiceProvider();
    }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        var mainWindow = _serviceProvider.GetService<MainWindow>();
        if (mainWindow == null)
        {
            return;
        }

        mainWindow.Show();
    }
}
