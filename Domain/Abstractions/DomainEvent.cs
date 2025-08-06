using MediatR;

namespace Domain.Abstractions;

public abstract class DomainEvent : INotification
{
    public Guid AggregateId { get; protected set; }
    public DateTime DateTime { get; protected set; } = DateTime.UtcNow;
}