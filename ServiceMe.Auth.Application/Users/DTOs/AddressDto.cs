namespace ServiceMe.Auth.Application.Users.DTOs;

public class AddressDto 
{
    public string Street { get; set; }
    public string City { get; set; }
    public string PostCode { get; set; }
    public string Country { get; set; }

    public AddressDto(string street, string city, string postCode, string country)
    {
        Street = street;
        City = city;
        PostCode = postCode;
        Country = country;
    }
}