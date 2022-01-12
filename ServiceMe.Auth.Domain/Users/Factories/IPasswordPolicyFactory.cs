using ServiceMe.Auth.Domain.Users.Policies;

namespace ServiceMe.Auth.Domain.Users.Factories;

public interface IPasswordPolicyFactory
{
    IPasswordPolicy Create();
}