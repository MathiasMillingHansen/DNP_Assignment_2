using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace EfcDataAccess.EfcDao;

public class EfcUserDao : IUserDao
{
    private readonly EfcRedditContext _context;

    public EfcUserDao(EfcRedditContext context)
    {
        _context = context;
    }

    public async Task<User> CreateAsync(User user)
    {
        var newUser = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task<User?> GetByUsername(string username)
    {
        var existing = await _context.Users.FirstOrDefaultAsync(u =>
            u.Username.ToLower().Equals(username.ToLower())
        );
        return existing;
    }

    public async Task<ICollection<User>> GetAllAsync()
    {
        ICollection<User> usersToReturn = await _context.Users.ToListAsync();
        return usersToReturn;
    }

    public async Task<User> CreateUser(User user)
    {
        var newUser = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return newUser.Entity;
    }
}