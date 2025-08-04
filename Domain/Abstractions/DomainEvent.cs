namespace Domain.Abstractions;

public abstract class DomainEvent
{
    public Guid AggregateId { get; protected set; }
    public DateTime DateTime { get; protected set; } = DateTime.UtcNow;
}