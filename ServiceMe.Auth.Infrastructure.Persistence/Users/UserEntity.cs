using PlaygroundShared.Infrastructure.MongoDb.Attribute;
using PlaygroundShared.Infrastructure.MongoDb.Entities;

namespace ServiceMe.Auth.Infrastructure.Persistence.Users;

[MongoCollection("Users")]
public class UserEntity : BaseMongoEntity
{
    public string UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public AddressEntity? Address { get; set; }
    public string? PhoneNumber { get; set; }
    
    public List<UserRoleEntity> Roles { get; set; }
}