using Shared.DTOs;
using Shared.DTOs.User;
using Shared.Models;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    Task<User> CreateUser(User user);

    Task<User> CreateAsync(User user);
    Task<User?> GetByUsername(string username);
    public Task<IEnumerable<User>> GetUser(SearchUserParametersDto searchParameters);
    Task<ICollection<User>> GetAllAsync();
}