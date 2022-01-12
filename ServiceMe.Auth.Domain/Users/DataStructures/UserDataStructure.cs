namespace ServiceMe.Auth.Domain.Users.DataStructures;

public class UserDataStructure
{
    public string UserName { get; }
    public string? FirstName { get; }
    public string? LastName { get; }
    public string Email { get; }
    public Address? Address { get; }
    public string? PhoneNumber { get; }

    public UserDataStructure(string userName, string firstName, string lastName, string email, Address? address, string phoneNumber)
    {
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Address = address;
        PhoneNumber = phoneNumber;
    }
}