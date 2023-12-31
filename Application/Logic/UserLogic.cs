using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.DTOs.User;
using Shared.Models;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }

    public async Task<User> CreateUserAsync(UserCreationDto userToCreate)
    {
        var existing = await userDao.GetByUsername(userToCreate.UserName);
        if (existing != null)
            throw new Exception("Username already taken!");

        ValidateData(userToCreate);
        var toCreate = new User(userToCreate.UserName, userToCreate.Password);

        var created = await userDao.CreateAsync(toCreate);

        return created;
    }

    public Task<IEnumerable<User>> GetUserAsync(SearchUserParametersDto searchParameters)
    {
        throw new NotImplementedException();
    }

    public async Task<GetUserWithPasswordDto?> GetUserByUsernameAsync(string userToGet)
    {
        var userToReturn = new GetUserWithPasswordDto(
            userDao.GetByUsername(userToGet).Result.Username,
            userDao.GetByUsername(userToGet).Result.Password);
        return userToReturn;
    }


    public async Task<ICollection<GetUserDto>> GetAllUsersAsync()
    {
        ICollection<GetUserDto> users = new List<GetUserDto>();
        foreach (var user in await userDao.GetAllAsync()) users.Add(new GetUserDto(user.Username, user.Password));

        return users;
    }

    public async Task<User> CreateUser(UserCreationDto userToCreate)
    {
        var existing = await userDao.GetByUsername(userToCreate.UserName);
        if (existing != null)
            throw new Exception("Username already taken!");
        ValidateData(userToCreate);
        var toCreate = new User(userToCreate.UserName, userToCreate.Password);

        var created = await userDao.CreateAsync(toCreate);

        return created;
    }

    private static void ValidateData(UserCreationDto userToCreate)
    {
        var userName = userToCreate.UserName;

        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (userName.Length > 15)
            throw new Exception("Username must be less than 16 characters!");
    }
}