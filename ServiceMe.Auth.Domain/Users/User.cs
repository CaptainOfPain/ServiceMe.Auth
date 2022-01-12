using PlaygroundShared.Domain.Domain;
using PlaygroundShared.Domain.DomainEvents;
using ServiceMe.Auth.Domain.Users.DataStructures;
using ServiceMe.Auth.Domain.Users.Events;
using ServiceMe.Auth.Domain.Users.Factories;

namespace ServiceMe.Auth.Domain.Users;

public class User : BaseAggregateRoot
{
    private IPasswordPolicyFactory _passwordPolicyFactory;
    
    public string UserName { get; private set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public Address? Address { get; private set; }
    public string? PhoneNumber { get; private set; }
    
    private List<UserRole> _roles = new();
    public IEnumerable<UserRole> Roles
    {
        get => _roles;
        private set => _roles = value.ToList();
    }

    private User()
    {
    }

    internal User(AggregateId id, IDomainEventsManager domainEventsManager, IPasswordPolicyFactory passwordPolicyFactory, UserDataStructure userDataStructure) : base(id, domainEventsManager)
    {
        SetDependencies(domainEventsManager, passwordPolicyFactory);
        AssignData(userDataStructure);
        DomainEvent(UserCreatedEvent.From(this));
    }

    public void Update(UserDataStructure userDataStructure)
    {
        AssignData(userDataStructure);
    }

    public void SavePassword(string password, string confirmPassword)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentNullException(nameof(password)); 
        }

        if (!password.Equals(confirmPassword))
        {
            throw new InvalidOperationException(nameof(confirmPassword));
        }

        PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, 14);
    }

    public bool VerifyPassword(string password)
        => BCrypt.Net.BCrypt.Verify(password, PasswordHash);

    public new void SetDependencies(IDomainEventsManager domainEventsManager,
        IPasswordPolicyFactory passwordPolicyFactory)
    {
        base.SetDependencies(domainEventsManager);
        _passwordPolicyFactory = passwordPolicyFactory;
    }
    
    private void AssignData(UserDataStructure userDataStructure)
    {
        SetUserName(userDataStructure.UserName);
        SetEmail(userDataStructure.Email);
        FirstName = userDataStructure.FirstName;
        LastName = userDataStructure.LastName;
        Address = userDataStructure.Address;
        PhoneNumber = userDataStructure.PhoneNumber;
    }

    private void SetUserName(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
        {
            throw new ArgumentNullException(nameof(userName));
        }

        UserName = userName;
    }

    private void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentNullException(nameof(email));
        }

        Email = email;
    }
}