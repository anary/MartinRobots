using Application.RobotCommands;
using Domain.Abstractions.Persistence;
using Domain.Commons;
using Domain.Services;
using Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    public static async Task Main(string[] args)
    {
        // Read grid setup
        var gridCoords = Console.ReadLine()?.Split(' ');
        int gridX = int.Parse(gridCoords[0]);
        int gridY = int.Parse(gridCoords[1]);

        // Setup Dependency Injection
        var serviceProvider = new ServiceCollection()
            .AddLogging()
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ProcessRobotInstructionsCommand).Assembly))
            .AddSingleton<IRobotRepository, InMemoryRobotRepository>()
            .AddSingleton(new MarsGridService(gridX, gridY))
            .BuildServiceProvider();

        var mediator = serviceProvider.GetRequiredService<IMediator>();

        string robotPositionInput;
        while (!string.IsNullOrEmpty(robotPositionInput = Console.ReadLine()))
        {
            var positionParts = robotPositionInput.Split(' ');
            var instructions = Console.ReadLine();

            // Create a command object with the input data
            var command = new ProcessRobotInstructionsCommand
            (
                int.Parse(positionParts[0]),
                int.Parse(positionParts[1]),
                Enum.Parse<Orientation>(positionParts[2]),
                instructions ?? ""
            );

            // Send the command to MediatR. It will be handled by the appropriate handler.
            var result = await mediator.Send(command);

            Console.WriteLine(result);
        }
    }
}
