using PlaygroundShared.Infrastructure.MongoDb.Entities;
using ServiceMe.Auth.Domain.Users;

namespace ServiceMe.Auth.Infrastructure.Persistence.Users;

public class UserRoleEntity : BaseMongoEntity
{
    public UserRoleType UserRoleType { get; set; }
}