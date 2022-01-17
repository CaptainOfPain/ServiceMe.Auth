using PlaygroundShared.Application.CQRS;
using ServiceMe.Auth.Application.Users.Commands;
using ServiceMe.Auth.Domain.Users;

namespace ServiceMe.Auth.Web;

public static class SeedExtensions
{
    public static WebApplication? SeedDatabase(this WebApplication? app)
    {
        var commandQueryDispatcher = app?.Services?.GetService<ICommandQueryDispatcherDecorator>();
        if (commandQueryDispatcher == null)
        {
            throw new ArgumentNullException(nameof(commandQueryDispatcher));
        }

        try
        {
            var command = new RegisterUserCommand(
                "admin",
                "admin@serviceme.com",
                null,
                null,
                "zaq1@WSX",
                "zaq1@WSX",
                null,
                null);
            commandQueryDispatcher.DispatchAsync(command).GetAwaiter().GetResult();

            commandQueryDispatcher.DispatchAsync(new AddUserRoleCommand(command.Id, UserRoleType.Admin));
        }
        catch
        {
            return app;
        }

        return app;
    }
}