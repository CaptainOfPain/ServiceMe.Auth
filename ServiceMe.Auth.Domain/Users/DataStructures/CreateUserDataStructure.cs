using PlaygroundShared.Domain.Domain;

namespace ServiceMe.Auth.Domain.Users.DataStructures;

public class CreateUserDataStructure : UserDataStructure
{
    public AggregateId Id { get; }

    public CreateUserDataStructure(AggregateId id, string userName, string firstName, string lastName, string email, Address? address, string phoneNumber) : base(userName, firstName, lastName, email, address, phoneNumber)
    {
        Id = id;
    }
}