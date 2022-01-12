using PlaygroundShared.Domain.Domain;
using PlaygroundShared.Domain.DomainEvents;
using ServiceMe.Auth.Domain.Users.Factories;

namespace ServiceMe.Auth.Domain.Users;

public class UserRecreate : IAggregateRecreate<User>
{
    private readonly IDomainEventsManager _domainEventsManager;
    private readonly IPasswordPolicyFactory _passwordPolicyFactory;

    public UserRecreate(IDomainEventsManager domainEventsManager, IPasswordPolicyFactory passwordPolicyFactory)
    {
        _domainEventsManager = domainEventsManager ?? throw new ArgumentNullException(nameof(domainEventsManager));
        _passwordPolicyFactory = passwordPolicyFactory ?? throw new ArgumentNullException(nameof(passwordPolicyFactory));
    }
    
    public void Init(User aggregate)
    {
        aggregate.SetDependencies(_domainEventsManager, _passwordPolicyFactory);
    }
}