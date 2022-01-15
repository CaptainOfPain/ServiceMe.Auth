using PlaygroundShared.Infrastructure.MongoDb.Attribute;
using PlaygroundShared.Infrastructure.MongoDb.Entities;

namespace ServiceMe.Auth.Infrastructure.Persistence.Users;

[MongoCollection("Users")]
public class UserEventEntity : BaseMongoEventEntity
{
    
}