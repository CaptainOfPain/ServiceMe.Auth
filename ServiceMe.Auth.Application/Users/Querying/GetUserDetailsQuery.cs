using PlaygroundShared.Application.CQRS;

namespace ServiceMe.Auth.Application.Users.Querying;

public class GetUserDetailsQuery : IQuery
{
    public Guid UserId { get; }

    public GetUserDetailsQuery(Guid userId)
    {
        UserId = userId;
    }
}