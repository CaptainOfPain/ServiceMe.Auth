using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlaygroundShared.Application.CQRS;
using PlaygroundShared.Domain.Domain;
using PlaygroundShared.Domain.DomainEvents;
using PlaygroundShared.Domain.Shared;
using PlaygroundShared.IntercontextCommunication;
using ServiceMe.Auth.Application.Users.Commands;
using ServiceMe.Auth.Application.Users.DTOs;
using ServiceMe.Auth.Application.Users.Querying;
using ServiceMe.Auth.Domain.Users;
using ServiceMe.Auth.Web.Requests;

namespace ServiceMe.Auth.Web.Controllers;

[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ICommandQueryDispatcherDecorator _commandQueryDispatcherDecorator;
    private readonly ICorrelationContext _correlationContext;

    public UsersController(ICommandQueryDispatcherDecorator commandQueryDispatcherDecorator, ICorrelationContext correlationContext)
    {
        _commandQueryDispatcherDecorator = commandQueryDispatcherDecorator ?? throw new ArgumentNullException(nameof(commandQueryDispatcherDecorator));
        _correlationContext = correlationContext ?? throw new ArgumentNullException(nameof(correlationContext));
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        var command = new RegisterUserCommand(
            request.UserName,
            request.Email,
            request.FirstName,
            request.LastName,
            request.Password,
            request.ConfirmPassword,
            request.Address != null
                ? new AddressData(request.Address.Street, request.Address.City, request.Address.PostCode,
                    request.Address.Country)
                : null,
            request.PhoneNumber);

        await _commandQueryDispatcherDecorator.DispatchAsync(command);
        return Ok();
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
    {
        var command = new UpdateUserCommand(
            new AggregateId(request.Id),
            request.FirstName,
            request.LastName,
            request.Address != null
                ? new AddressData(request.Address.Street, request.Address.City, request.Address.PostCode,
                    request.Address.Country)
                : null,
            request.PhoneNumber
        );
        await _commandQueryDispatcherDecorator.DispatchAsync(command);
        return Ok();
    }

    [HttpPost("signIn")]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
    {
        var query = new GetUserJwtQuery(request.EmailOrUserName, request.EmailOrUserName, request.Password);
        var result = await _commandQueryDispatcherDecorator.DispatchAsync<GetUserJwtQuery, UserLoginDto>(query);

        return Ok(result);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        var query = new GetUserDetailsQuery(_correlationContext.CurrentUser.UserId.Value.Id);
        var result = await _commandQueryDispatcherDecorator.DispatchAsync<GetUserDetailsQuery, UserDetailsDto>(query);

        return Ok(result);
    }

    [HttpPost("AddRole")]
    [Authorize(Roles = nameof(UserRoleType.Admin))]
    public async Task<IActionResult> AddRole([FromBody] AddRoleToUserRequest request)
    {
        var command = new AddUserRoleCommand(new AggregateId(request.UserId), request.UserRoleType);
        await _commandQueryDispatcherDecorator.DispatchAsync(command);

        return Ok();
    }
}