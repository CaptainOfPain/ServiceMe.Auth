using AutoMapper;
using PlaygroundShared.Domain.Domain;
using PlaygroundShared.Domain.DomainEvents;
using PlaygroundShared.Infrastructure.Core.Repositories;
using ServiceMe.Auth.Domain.Users;
using ServiceMe.Auth.Domain.Users.Repositories;

namespace ServiceMe.Auth.Infrastructure.Persistence.Users;

public class UserRepository : BaseAggregateRootRepository<User, UserEntity, UserEventEntity>, IUserRepository
{
    public UserRepository(IGenericRepository<UserEntity> repository, IGenericEventRepository<UserEventEntity> eventRepository, IDomainEventsManager domainEventsManager, IMapper mapper, IAggregateRecreate<User> aggregateRecreate) : base(repository, eventRepository, domainEventsManager, mapper, aggregateRecreate)
    {
    }

    public async Task<User?> GetUserByUserNameOrEmailAsync(string userName, string email)
        => Mapper.Map<User?>(await Repository.GetByExpressionAsync(x => x.UserName == userName || x.Email == email));
}