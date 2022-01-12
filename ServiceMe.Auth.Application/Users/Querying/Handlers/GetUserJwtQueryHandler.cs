using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PlaygroundShared.Application.CQRS;
using PlaygroundShared.Configurations;
using PlaygroundShared.Domain.Domain;
using ServiceMe.Auth.Application.Users.DTOs;
using ServiceMe.Auth.Domain.Users;
using ServiceMe.Auth.Domain.Users.Repositories;

namespace ServiceMe.Auth.Application.Users.Querying.Handlers;

public class GetUserJwtQueryHandler : IQueryHandler<GetUserJwtQuery, UserLoginDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtConfiguration _jwtConfiguration;

    public GetUserJwtQueryHandler(IUserRepository userRepository, IJwtConfiguration jwtConfiguration)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _jwtConfiguration = jwtConfiguration ?? throw new ArgumentNullException(nameof(jwtConfiguration));
    }
    
    public async Task<UserLoginDto> HandleAsync(GetUserJwtQuery query)
    {
        var user = await _userRepository.GetUserByUserNameOrEmailAsync(query.UserName, query.Email);
        if (user == null) throw new Exception();

        if (!user.VerifyPassword(query.Password))
        {
            throw new Exception();
        }
        
        return new UserLoginDto(user.Id.ToGuid(), user.FirstName, user.LastName, user.UserName, user.Email, GenerateJwtToken(user));
    }

    private string GenerateJwtToken(User user)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtConfiguration.Secret);

        var claimTypes = new List<Claim>()
        {
            new("id", user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName),
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        claimTypes.AddRange(user.Roles.Select(x => new Claim(ClaimTypes.Role, x.Type.ToString())));
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claimTypes),
            Expires = DateTime.UtcNow.AddHours(_jwtConfiguration.ExpiresHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = jwtTokenHandler.WriteToken(token);

        return jwtToken;
    }
}

public class GetUserDetailsQueryHandler : IQueryHandler<GetUserDetailsQuery, UserDetailsDto>
{
    private readonly IUserRepository _userRepository;

    public GetUserDetailsQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }
    
    public async Task<UserDetailsDto> HandleAsync(GetUserDetailsQuery query)
    {
        var user = await _userRepository.GetAsync(new AggregateId(query.UserId));

        return new(
            user.UserName,
            user.FirstName,
            user.LastName,
            user.Email,
            user.Address.HasValue
                ? new AddressDto(
                    user.Address.Value.Street,
                    user.Address.Value.City,
                    user.Address.Value.PostCode,
                    user.Address.Value.Country)
                : null,
            user.Roles.Select(x => x.Type.ToString()),
            user.PhoneNumber);
    }
}