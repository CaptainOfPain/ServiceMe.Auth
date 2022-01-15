using MongoDB.Driver;
using PlaygroundShared.Configurations;
using PlaygroundShared.Infrastructure.MongoDb.Repositories;
using ServiceMe.Auth.Infrastructure.Persistence.Users;

namespace ServiceMe.Auth.Infrastructure.MongoDb.Users;

public class UserMongoEventRepository : GenericMongoEventRepository<UserEventEntity>
{
    public UserMongoEventRepository(IMongoClient mongoClient, IMongoDbConfiguration mongoDbConfiguration) : base(mongoClient, mongoDbConfiguration)
    {
    }
}