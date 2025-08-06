using Domain.Robots;

namespace Domain.Abstractions.Persistence;
public interface IRobotRepository
{
    Task<Robot> GetRobotAsync(Guid id);
    Task SaveRobotAsync(Robot robot, CancellationToken cancellationToken);
}