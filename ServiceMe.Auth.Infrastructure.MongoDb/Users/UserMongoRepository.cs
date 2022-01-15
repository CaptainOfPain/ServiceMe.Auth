using MongoDB.Driver;
using PlaygroundShared.Configurations;
using PlaygroundShared.Infrastructure.MongoDb.Repositories;
using ServiceMe.Auth.Infrastructure.Persistence.Users;

namespace ServiceMe.Auth.Infrastructure.MongoDb.Users;

public class UserMongoRepository : GenericMongoRepository<UserEntity>
{
    public UserMongoRepository(IMongoClient mongoClient, IMongoDbConfiguration mongoDbConfiguration) : base(mongoClient, mongoDbConfiguration)
    {
    }
}