using Application.RobotCommands;
using Domain.Abstractions.Persistence;
using Domain.Services;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class ServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, int gridX, int gridY)
    {
        return services
            .AddLogging()
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ProcessRobotInstructionsCommand).Assembly))
            .AddSingleton<IRobotRepository, InMemoryRobotRepository>()
            .AddSingleton(new MarsGridService(gridX, gridY));

    }
}
