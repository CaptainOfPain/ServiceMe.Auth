using PlaygroundShared.Domain.Domain;
using PlaygroundShared.Domain.DomainEvents;

namespace ServiceMe.Auth.Domain.Users.Events;

public class UserPasswordChangedEvent : IDomainEvent
{
    public Guid CorrelationId { get; set; }
    public AggregateId Id { get; }

    private UserPasswordChangedEvent(AggregateId id)
    {
        Id = id;
    }

    public static UserPasswordChangedEvent Create(User user)
        => new UserPasswordChangedEvent(user.Id);
}