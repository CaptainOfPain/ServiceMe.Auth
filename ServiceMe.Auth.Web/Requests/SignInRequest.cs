namespace ServiceMe.Auth.Web.Requests;

public class SignInRequest
{
    public string EmailOrUserName { get; set; }
    public string Password { get; set; }
}