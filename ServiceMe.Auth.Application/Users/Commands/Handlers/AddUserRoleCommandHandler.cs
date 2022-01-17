using PlaygroundShared.Application.CQRS;
using PlaygroundShared.Domain.Exceptions;
using ServiceMe.Auth.Domain.Users.Repositories;

namespace ServiceMe.Auth.Application.Users.Commands.Handlers;

public class AddUserRoleCommandHandler : ICommandHandler<AddUserRoleCommand>
{
    private readonly IUserRepository _userRepository;

    public AddUserRoleCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }
    public async Task HandleAsync(AddUserRoleCommand command)
    {
        var user = (await _userRepository.GetAsync(command.Id)) ?? throw new BusinessLogicException();
        user.AddRole(command.UserRoleType);

        await _userRepository.PersistAsync(user);
    }
}