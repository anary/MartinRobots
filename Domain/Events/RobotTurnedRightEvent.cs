using Domain.Abstractions;
using Domain.Commons;

namespace Domain.Events;
internal class RobotTurnedRightEvent : DomainEvent
{
    public Orientation NewOrientation { get; }
    public RobotTurnedRightEvent(Guid id, Orientation orientation)
    {
        AggregateId = id;
        NewOrientation = orientation;
    }
}
{
}
