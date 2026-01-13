using Microsoft.Extensions.DependencyInjection;
using GestureCommandEngine.Core.Interfaces;
using GestureCommandEngine.Core.Services;

namespace GestureCommandEngine.Core;

public static class ServicesConfiguration
{
    public static void ConfigureServices(ServiceCollection services)
    {
        services.AddSingleton<IMouseGestureRecognitionService, MouseGestureRecognitionService>();
        services.AddSingleton<ICommandsRepository, CommandsRepository>();
        services.AddSingleton<IGestureCommandsRepository, GestureCommandsRepository>();
        services.AddSingleton<IGestureCommandsHandler, GestureCommandsHandler>();
    }
}
