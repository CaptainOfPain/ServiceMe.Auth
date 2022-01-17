using PlaygroundShared.Domain.Domain;
using PlaygroundShared.Domain.DomainEvents;

namespace ServiceMe.Auth.Domain.Users.Events;

public class UserRolesUpdatedEvent : IDomainEvent
{
    public Guid CorrelationId { get; set; }
    public AggregateId Id { get; }
    public IEnumerable<UserRoleType> UserRoles { get; }

    private UserRolesUpdatedEvent(AggregateId id, IEnumerable<UserRoleType> userRoles)
    {
        Id = id;
        UserRoles = userRoles;
    }

    public static UserRolesUpdatedEvent Create(User user) => new(user.Id, user.Roles.Select(x => x.Type));
}