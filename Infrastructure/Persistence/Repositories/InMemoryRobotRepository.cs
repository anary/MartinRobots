using Domain.Abstractions;
using Domain.Abstractions.Persistence;
using Domain.Robots;
using MediatR;

namespace Infrastructure.Persistence.Repositories;
public class InMemoryRobotRepository : IRobotRepository
{
    private readonly IMediator _mediator;

    public InMemoryRobotRepository(IMediator mediator)
    {
        _mediator = mediator;
    }

    private readonly Dictionary<Guid, List<DomainEvent>> _eventStore = [];
    public Task<Robot> GetRobotAsync(Guid id)
    {
        if (!_eventStore.ContainsKey(id))
        {
            throw new Exception($"Robot with ID {id} not found.");
        }

        var robot = new Robot();
        robot.LoadFromHistory(_eventStore[id]);
        return Task.FromResult(robot);
    }

    public async Task SaveRobotAsync(Robot robot, CancellationToken cancellationToken)
    {
        IReadOnlyCollection<DomainEvent> unCommittedEvents = robot.GetUncommittedEvents().ToList();
        if (!unCommittedEvents.Any())
        {
            return;
        }

        if (!_eventStore.ContainsKey(robot.Id))
        {
            _eventStore[robot.Id] = new List<DomainEvent>();
        }
        _eventStore[robot.Id].AddRange(unCommittedEvents);

        foreach (var @event in unCommittedEvents)
        {
            await _mediator.Publish(@event);
        }

        robot.ClearUncommittedEvents();
    }
}
