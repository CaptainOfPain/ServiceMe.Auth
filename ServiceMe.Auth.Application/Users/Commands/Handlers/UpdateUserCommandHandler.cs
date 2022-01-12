using PlaygroundShared.Application.CQRS;
using ServiceMe.Auth.Domain.Users.Factories;
using ServiceMe.Auth.Domain.Users.Repositories;

namespace ServiceMe.Auth.Application.Users.Commands.Handlers;

public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository, IUserFactory userFactory)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }
    
    public async Task HandleAsync(UpdateUserCommand command)
    {
        var user = await _userRepository.GetAsync(command.Id);
        if (user == null) throw new Exception();
        
        user.Update(command.ToUserDataStructure(user.UserName, user.Email));

        await _userRepository.PersistAsync(user);
    }
}