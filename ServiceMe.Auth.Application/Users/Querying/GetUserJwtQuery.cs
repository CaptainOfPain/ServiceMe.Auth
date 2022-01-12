using PlaygroundShared.Application.CQRS;

namespace ServiceMe.Auth.Application.Users.Querying;

public class GetUserJwtQuery : IQuery
{
    public string UserName { get; }
    public string Email { get; }
    public string Password { get; }

    public GetUserJwtQuery(string userName, string email, string password)
    {
        UserName = userName;
        Email = email;
        Password = password;
    }
}