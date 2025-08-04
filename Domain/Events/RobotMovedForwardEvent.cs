using Domain.Abstractions;

namespace Domain.Events;
public class RobotMovedForwardEvent : DomainEvent
{
    public int NewX { get; }
    public int NewY { get; }
    public RobotMovedForwardEvent(Guid id, int x, int y)
    {
        AggregateId = id;
        NewX = x;
        NewY = y;
    }
}
