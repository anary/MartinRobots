using Domain.Abstractions;
using Domain.Commons;

namespace Domain.Events;
internal class RobotLandedEvent : DomainEvent
{
    public string X { get; }
    public string Y { get; }
    public Orientation Orientation { get; }
    public RobotLandedEvent(Guid id, string x, string y, Orientation orientation)
    {
        AggregateId = id;
        X = x;
        Y = y;
        Orientation = orientation;
    }
}
{
}
