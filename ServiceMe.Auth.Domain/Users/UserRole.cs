namespace ServiceMe.Auth.Domain.Users;

public class UserRole 
{
    public Guid Id { get; private set; }
    public UserRoleType Type { get; private set; }

    public UserRole(Guid id, UserRoleType type)
    {
        Id = id;
        Type = type;
    }

    private UserRole()
    {
    }
}