using ServiceMe.Auth.Domain.Users;
using ServiceMe.Auth.Domain.Users.DataStructures;

namespace ServiceMe.Auth.Application.Users.Commands;

public static class UserExplicitMapping
{
    public static CreateUserDataStructure ToCreateUserDataStructure(this RegisterUserCommand command)
        => new(command.Id, command.UserName, command.FirstName, command.LastName, command.Email,
            command.Address != null ? new Address(command.Address.Street, command.Address.City, command.Address.PostCode,
                command.Address.Country) : null,
            command.Phone);

    public static UserDataStructure ToUserDataStructure(this UpdateUserCommand command, string userName, string email)
        => new(userName, command.FirstName, command.LastName, email, command.Address != null ? new Address(command.Address.Street, command.Address.City, command.Address.PostCode,
            command.Address.Country) : null, command.PhoneNumber);
}