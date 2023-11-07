using Application.DaoInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace FileData.DAOs;

public class UserFileDao : IUserDao
{
    private readonly FileContext context;

    public UserFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<User> CreateAsync(User user)
    {
        if (context.Users.Any())
        {
            foreach (User _user in context.Users)
            {
                if (_user.Username.Equals(user.Username))
                {
                    throw new ArgumentException("Username unavailable");
                }
            }
        }

        context.Users.Add(user);
        context.SaveChanges();

        return Task.FromResult(user);
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
        );
        return Task.FromResult(existing);
    }

    public Task<User> CreateUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByUsername(string userName)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.Username.Equals(userName, StringComparison.OrdinalIgnoreCase)
        );
        return Task.FromResult(existing);;
    }

    public Task<IEnumerable<User>> GetUser(SearchUserParametersDto searchParameters)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<User>> GetAllAsync()
    {
        return Task.FromResult(context.Users);
    }
}