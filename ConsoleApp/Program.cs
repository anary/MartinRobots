using Application.RobotCommands;
using Domain.Commons;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            // Read grid setup
            var gridCoords = Console.ReadLine()?.Split(' ');
            int gridX = int.Parse(gridCoords[0]);
            int gridY = int.Parse(gridCoords[1]);

            // Setup Dependency Injection
            var serviceProvider = new ServiceCollection()
                    .AddApplicationServices(gridX, gridY)
                    .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();

            await StartRobot(mediator);
        }
        catch (Exception)
        {

            Console.WriteLine("Error: Invalid input, please check the input format");
        }

    }

    private static async Task StartRobot(IMediator mediator)
    {
        string robotPositionInput;
        while (!string.IsNullOrEmpty(robotPositionInput = Console.ReadLine()))
        {
            var positionParts = robotPositionInput.Split(' ');
            var instructions = Console.ReadLine();
            string result = await InstructRobot(mediator, positionParts, instructions);

            Console.WriteLine(result);
        }
    }

    private static async Task<string> InstructRobot(IMediator mediator, string[] positionParts, string? instructions)
    {
        // Create a command object with the input data
        var command = new ProcessRobotInstructionsCommand
        (
            int.Parse(positionParts[0]),
            int.Parse(positionParts[1]),
            Enum.Parse<Orientation>(positionParts[2]),
            instructions ?? ""
        );

        return await mediator.Send(command);
    }
}
