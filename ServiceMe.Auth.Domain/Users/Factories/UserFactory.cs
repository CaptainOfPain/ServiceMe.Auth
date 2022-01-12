using PlaygroundShared.Domain.DomainEvents;
using ServiceMe.Auth.Domain.Users.DataStructures;

namespace ServiceMe.Auth.Domain.Users.Factories;

public class UserFactory : IUserFactory
{
    private readonly IDomainEventsManager _domainEventsManager;
    private readonly IPasswordPolicyFactory _passwordPolicyFactory;

    public UserFactory(IDomainEventsManager domainEventsManager, IPasswordPolicyFactory passwordPolicyFactory)
    {
        _domainEventsManager = domainEventsManager ?? throw new ArgumentNullException(nameof(domainEventsManager));
        _passwordPolicyFactory = passwordPolicyFactory ?? throw new ArgumentNullException(nameof(passwordPolicyFactory));
    }

    public User Create(CreateUserDataStructure userDataStructure)
        => new(userDataStructure.Id, _domainEventsManager, _passwordPolicyFactory, userDataStructure);
}