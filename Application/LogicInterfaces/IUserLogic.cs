using Shared.DTOs;
using Shared.DTOs.User;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User>CreateUserAsync(UserCreationDto userToCreate);
    Task<ICollection<GetUserDto>> GetAllUsersAsync();
    Task<IEnumerable<User>>GetUserAsync(SearchUserParametersDto searchParameters);
    
    Task<GetUserWithPasswordDto?> GetUserByUsernameAsync(string userToGet);
}