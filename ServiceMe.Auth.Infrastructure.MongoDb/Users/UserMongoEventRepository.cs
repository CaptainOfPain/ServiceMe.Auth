using PlaygroundShared.Infrastructure.MongoDb;
using PlaygroundShared.Infrastructure.MongoDb.Repositories;
using ServiceMe.Auth.Infrastructure.Persistence.Users;

namespace ServiceMe.Auth.Infrastructure.MongoDb.Users;

public class UserMongoEventRepository : GenericMongoEventRepository<UserEventEntity>
{
    public UserMongoEventRepository(IEventMongoDatabase eventMongoDatabase) : base(eventMongoDatabase)
    {
    }
}