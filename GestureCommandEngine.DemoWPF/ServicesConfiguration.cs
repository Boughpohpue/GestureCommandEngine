using Microsoft.Extensions.DependencyInjection;

namespace GestureCommandEngine.DemoWPF;

public static class ServicesConfiguration
{
    public static void ConfigureServices(ServiceCollection services)
    {
        Core.ServicesConfiguration.ConfigureServices(services);

        services.AddSingleton<MainWindow, MainWindow>();
    }
}
