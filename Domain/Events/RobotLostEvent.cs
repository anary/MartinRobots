using Domain.Abstractions;

namespace Domain.Events;
public class RobotLostEvent : DomainEvent
{
    public RobotLostEvent(Guid id)
    {
        AggregateId = id;
    }
}
