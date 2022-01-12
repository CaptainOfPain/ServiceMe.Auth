using PlaygroundShared.Application.CQRS;
using ServiceMe.Auth.Domain.Users.Factories;
using ServiceMe.Auth.Domain.Users.Repositories;

namespace ServiceMe.Auth.Application.Users.Commands.Handlers;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserFactory _userFactory;

    public RegisterUserCommandHandler(IUserRepository userRepository, IUserFactory userFactory)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _userFactory = userFactory ?? throw new ArgumentNullException(nameof(userFactory));
    }
    
    public async Task HandleAsync(RegisterUserCommand command)
    {
        var user = await _userRepository.GetUserByUserNameOrEmailAsync(command.UserName, command.Email);
        if (user != null) throw new Exception();
        
        user = _userFactory.Create(command.ToCreateUserDataStructure());
        user.SavePassword(command.Password, command.ConfirmPassword);

        await _userRepository.PersistAsync(user);
    }
}