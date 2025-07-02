namespace Application.DTOs;

public record UserDto(
    string Id,
    string UserName,
    string Email,
    IEnumerable<string> Roles
);
