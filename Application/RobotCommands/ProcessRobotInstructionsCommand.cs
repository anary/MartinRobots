using Domain.Commons;
using MediatR;

namespace Application.RobotCommands;
public sealed record ProcessRobotInstructionsCommand(
    int StartX, int StartY,
    Orientation StartOrientation, string Instructions) : IRequest<string>;
