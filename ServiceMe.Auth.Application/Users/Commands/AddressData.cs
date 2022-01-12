namespace ServiceMe.Auth.Application.Users.Commands;

public class AddressData
{
    public string Street { get; }
    public string City { get; }
    public string PostCode { get; }
    public string Country { get; }

    public AddressData(string street, string city, string postCode, string country)
    {
        Street = street;
        City = city;
        PostCode = postCode;
        Country = country;
    }
}