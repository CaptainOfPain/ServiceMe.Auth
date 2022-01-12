namespace ServiceMe.Auth.Application.Users.DTOs;

public class UserDetailsDto
{
    public string UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }
    public AddressDto? Address { get; set; }
    public IEnumerable<string> Roles { get; set; }
    public string? PhoneNumber { get; set; }

    public UserDetailsDto(string userName, string? firstName, string? lastName, string email, AddressDto? address, IEnumerable<string> roles, string? phoneNumber)
    {
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Address = address;
        Roles = roles;
        PhoneNumber = phoneNumber;
    }
}