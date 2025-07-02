namespace Application.Features.Users.Commands.Register;

public record RegisterCommand(
    string UserName,
    string Email,
    string Password) : ICommand;