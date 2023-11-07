using Shared.Models;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByUsername(string username);
    Task<ICollection<User>> GetAllAsync();
}