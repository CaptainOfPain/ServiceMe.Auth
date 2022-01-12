using PlaygroundShared.Domain.Domain;

namespace ServiceMe.Auth.Domain.Users.Events;

public class UserUpdatedEvent
{
    public AggregateId Id { get; }
    public string UserName { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public Address? Address { get; }
    public string PhoneNumber { get; }
    public Guid CorrelationId { get; set; }

    private UserUpdatedEvent(AggregateId id, string userName, string? firstName, string? lastName, string email, Address? address, string? phoneNumber)
    {
        Id = id;
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Address = address;
        PhoneNumber = phoneNumber;
    }

    public static UserUpdatedEvent From(User user) => new(user.Id, user.UserName, user.FirstName, user.LastName, user.Email,
        user.Address, user.PhoneNumber);
}