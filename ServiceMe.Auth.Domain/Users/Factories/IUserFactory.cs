using PlaygroundShared.Domain.Domain;
using ServiceMe.Auth.Domain.Users.DataStructures;

namespace ServiceMe.Auth.Domain.Users.Factories;

public interface IUserFactory : IDomainFactory
{
    User Create(CreateUserDataStructure userDataStructure);
}