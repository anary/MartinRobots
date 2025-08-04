using Domain.Abstractions;
using Domain.Commons;

namespace Domain.Events;
public class RobotTurnedLeftEvent : DomainEvent
{
    public Orientation NewOrientation { get; }
    public RobotTurnedLeftEvent(Guid id, Orientation orientation)
    {
        AggregateId = id;
        NewOrientation = orientation;
    }
}
