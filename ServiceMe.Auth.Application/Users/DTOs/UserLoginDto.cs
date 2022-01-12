namespace ServiceMe.Auth.Application.Users.DTOs;

public class UserLoginDto
{
    public Guid Id { get; }
    public string? FirstName { get; }
    public string? LastName { get; }
    public string UserName { get; }
    public string Email { get; }
    public string JwtToken { get; }

    public UserLoginDto(Guid id, string? firstName, string? lastName, string userName, string email, string jwtToken)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        Email = email;
        JwtToken = jwtToken;
    }
}