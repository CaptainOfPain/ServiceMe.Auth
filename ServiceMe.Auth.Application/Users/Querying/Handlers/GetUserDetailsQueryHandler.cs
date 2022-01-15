using PlaygroundShared.Application.CQRS;
using PlaygroundShared.Domain.Domain;
using ServiceMe.Auth.Application.Users.DTOs;
using ServiceMe.Auth.Domain.Users.Repositories;

namespace ServiceMe.Auth.Application.Users.Querying.Handlers;

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