using PlaygroundShared.Application.CQRS;
using PlaygroundShared.Domain.Domain;
using ServiceMe.Auth.Domain.Users;

namespace ServiceMe.Auth.Application.Users.Commands;

public class UpdateUserCommand : ICommand
{
    public AggregateId Id { get; }
    public string? FirstName { get; }
    public string? LastName { get; }
    public AddressData? Address { get; }
    public string? PhoneNumber { get; }

    public UpdateUserCommand(AggregateId id, string? firstName, string? lastName, AddressData? address, string? phoneNumber)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        PhoneNumber = phoneNumber;
    }
}