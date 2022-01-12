using PlaygroundShared.Application.CQRS;
using PlaygroundShared.Domain.Domain;

namespace ServiceMe.Auth.Application.Users.Commands;

public class RegisterUserCommand : ICommand
{
    public AggregateId Id { get; }
    public string UserName { get; }
    public string Email { get; }
    public string? FirstName { get; }
    public string? LastName { get; }
    public string Password { get; }
    public string ConfirmPassword { get; }
    public AddressData? Address { get; }
    public string? Phone { get; }

    public RegisterUserCommand(string userName, string email, string? firstName, string? lastName, string password, string confirmPassword, AddressData? address, string? phone)
    {
        Id = AggregateId.Generate();
        UserName = userName;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
        ConfirmPassword = confirmPassword;
        Address = address;
        Phone = phone;
    }
}