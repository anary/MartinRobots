using Domain.Abstractions;
using Domain.Commons;

namespace Domain.Events;
internal class RobotLandedEvent : DomainEvent
{
    public int X { get; }
    public int Y { get; }
    public Orientation Orientation { get; }
    public RobotLandedEvent(Guid id, int x, int y, Orientation orientation)
    {
        AggregateId = id;
        X = x;
        Y = y;
        Orientation = orientation;
    }
}
