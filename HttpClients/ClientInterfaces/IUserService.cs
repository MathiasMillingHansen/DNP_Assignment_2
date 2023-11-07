using Shared.DTOs;
using Shared.Models;

namespace Clients.ClientInterfaces;

public interface IUserService
{
    Task<User> CreateUserAsync(UserCreationDto userToCreate);
    Task<IEnumerable<User>> GetUsersAsync(string? usernameContains = null);
}