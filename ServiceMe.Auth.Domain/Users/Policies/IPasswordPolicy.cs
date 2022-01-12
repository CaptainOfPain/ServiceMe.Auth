namespace ServiceMe.Auth.Domain.Users.Policies;

public interface IPasswordPolicy
{
    bool ValidatePassword(string password, string confirmPassword);
}