using ServiceMe.Auth.Domain.Users.Policies;

namespace ServiceMe.Auth.Domain.Users.Factories;

public class PasswordPolicyFactory : IPasswordPolicyFactory
{
    public IPasswordPolicy Create()
        => new BasicPasswordPolicy();
}