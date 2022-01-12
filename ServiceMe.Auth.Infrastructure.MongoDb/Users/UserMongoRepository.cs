using PlaygroundShared.Infrastructure.MongoDb;
using PlaygroundShared.Infrastructure.MongoDb.Repositories;
using ServiceMe.Auth.Infrastructure.Persistence.Users;

namespace ServiceMe.Auth.Infrastructure.MongoDb.Users;

public class UserMongoRepository : GenericMongoRepository<UserEntity>
{
    public UserMongoRepository(IMainMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
    }
}