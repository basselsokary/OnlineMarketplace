namespace Application.Features.Users.Commands.Login;

public record LoginCommand(
    string Email,
    string Password,
    bool RememberMe = false) : ICommand;
