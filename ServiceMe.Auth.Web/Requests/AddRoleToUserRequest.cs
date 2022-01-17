using ServiceMe.Auth.Domain.Users;

namespace ServiceMe.Auth.Web.Requests;

public class AddRoleToUserRequest
{
    public Guid UserId { get; set; }
    public UserRoleType UserRoleType { get; set; }
}