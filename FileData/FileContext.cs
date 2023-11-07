using System.Text.Json;
using FileData;
using Shared.Models;

namespace FileData;

public class FileContext
{
    private const string UserFilePath = "users.json";
    private const string PostFilePath = "posts.json";
    private UserContainer? _userContainer;
    private PostContainer? _postContainer;
    

    public ICollection<RedditPost> Posts
    {
        get
        {
            LoadPosts();

            return _postContainer!.Posts;
        }
    }

    public ICollection<User> Users
    {
        get
        {
            LoadUsers();
            return _userContainer!.Users;
        }
    }

    private void LoadUsers()
    {
        if (_userContainer != null) return;

        if (!File.Exists(UserFilePath))
        {
            _userContainer = new()
            {
                Users = new List<User>()
            };
            return;
        }

        string users = File.ReadAllText(UserFilePath);
        _userContainer = JsonSerializer.Deserialize<UserContainer>(users);
    }

    private void LoadPosts()
    {
        if (_postContainer != null) return;
        
        if (!File.Exists(PostFilePath))
        {
            _postContainer = new()
            {
                Posts = new List<RedditPost>()
            };
            return;
        }
        string posts = File.ReadAllText(PostFilePath);
/*        if (posts.Equals("null"))
        {
            _postContainer = new PostContainer
            {
                Posts = new List<RedditPost>()
            };
        }
        else
        {*/
            _postContainer = JsonSerializer.Deserialize<PostContainer>(posts);
        //}
    }

    public void SaveChanges()
    {
        if (_userContainer is not null)
        {
            string usersSerialized = JsonSerializer.Serialize(_userContainer, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(UserFilePath, usersSerialized);
        }

        if (_postContainer is not null)
        {
            string postsSerialized = JsonSerializer.Serialize(_postContainer, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(PostFilePath, postsSerialized);
        }
        
        _userContainer = null;
        _postContainer = null;
    }


    public void UpdateUserPostList(RedditPost redditPost)
    {
        LoadUsers();
        foreach (User user in _userContainer!.Users)
        {
            if (user.Username.Equals(redditPost.User.Username))
            {
                user.RedditPosts.Add(redditPost);
                SaveChanges();
            }
        }
    }
}