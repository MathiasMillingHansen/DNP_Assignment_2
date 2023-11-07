using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DTOs;
using Shared.Models;

namespace EfcDataAccess.EfcDao;

public class EfcUserDao : IUserDao
{
    
    private readonly EfcRedditContext _context;
    
    public EfcUserDao(EfcRedditContext context)
    {
        _context = context;
    }
    
    public async Task<User> CreateUser(User user)
    {
        EntityEntry<User> newUser = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task<User> CreateAsync(User user)
    {
        EntityEntry<User> newUser = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task<User?> GetByUsername(string username)
    {
        User? existing = await _context.Users.FirstOrDefaultAsync(u =>
            u.Username.ToLower().Equals(username.ToLower())
        );
        return existing;
    }

    public async Task<ICollection<User>> GetAllAsync()
    {
        ICollection<User> usersToReturn = await _context.Users.ToListAsync();
        return usersToReturn;
    }
}