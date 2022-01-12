namespace ServiceMe.Auth.Domain.Users.Policies;

public class BasicPasswordPolicy : IPasswordPolicy
{
    public bool ValidatePassword(string password, string confirmPassword)
        => password.Equals(confirmPassword) && password.Length >= 8;
}