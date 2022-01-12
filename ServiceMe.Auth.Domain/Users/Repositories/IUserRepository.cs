using PlaygroundShared.Domain.Domain;

namespace ServiceMe.Auth.Domain.Users.Repositories;

public interface IUserRepository : IAggregateRepository<User>
{
    Task<User?> GetUserByUserNameOrEmailAsync(string userName, string email);
}