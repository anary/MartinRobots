namespace Domain.Abstractions;
public abstract class AggregateRoot
{
    public Guid Id { get; protected set; }
    public int Version { get; protected set; } = -1;
    private readonly List<DomainEvent> _uncommittedEvents = new List<DomainEvent>();

    protected void ApplyChange(DomainEvent @event)
    {
        When((dynamic)@event);
        _uncommittedEvents.Add(@event);
    }

    public void LoadFromHistory(IEnumerable<DomainEvent> history)
    {
        foreach (var e in history)
        {
            When((dynamic)e);
            Version++;
        }
    }

    protected abstract void When(DomainEvent @event);
}
