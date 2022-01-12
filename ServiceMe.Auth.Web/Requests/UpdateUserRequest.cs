namespace ServiceMe.Auth.Web.Requests;

public class UpdateUserRequest
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public AddressRequestData? Address { get; set; }
    public string? PhoneNumber { get; set; }
}