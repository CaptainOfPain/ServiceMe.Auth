using PlaygroundShared.Application.CQRS;
using PlaygroundShared.Domain.Domain;
using ServiceMe.Auth.Domain.Users;

namespace ServiceMe.Auth.Application.Users.Commands;

public class AddUserRoleCommand : ICommand
{
    public AggregateId Id { get; }
    public UserRoleType UserRoleType { get; }

    public AddUserRoleCommand(AggregateId id, UserRoleType userRoleType)
    {
        Id = id;
        UserRoleType = userRoleType;
    }
}