namespace BuildingBlocks.Messaging.Events;
public record IntegrationEvent
{
    public Guid Id => Guid.NewGuid();
    public DateTime OnOccurredOn => DateTime.UtcNow;
    public string EventType => GetType().AssemblyQualifiedName;
}