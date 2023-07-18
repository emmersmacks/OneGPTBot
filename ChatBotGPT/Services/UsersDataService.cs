using ChatBotGPT.Database;
using ChatBotGPT.Database.Models;
using Telegram.Bot.Types;

namespace VideoBot.Services;

public class UsersDataService
{
    private readonly ApplicationContext _applicationContext;
    public Dictionary<long, UserModel> Users;

    public UsersDataService(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
        FillUsersDict();
    }

    public void UpdateUser(UserModel userModel)
    {
        var user = Users[userModel.Id];
        if(user == null)
            throw new Exception($"{nameof(this.GetType)}: -20 | User for update not found!");

        Users[userModel.Id] = userModel;

        _applicationContext.Users.Update(userModel);
        _applicationContext.SaveChanges();
        
    }

    public UserModel GetOrCreateUser(Message message)
    {
        var user = GetUser(message.From.Id);
        if (user == null)
        {
            user = CreateDefaultUser(message);
        }

        return user;
    }

    public UserModel GetUser(long id)
    {
        if (!Users.ContainsKey(id))
            return null;
        return Users[id];
    }

    public UserModel CreateDefaultUser(Message message)
    {
        var user = new UserModel()
        {
            Id = message.From.Id,
            FirstName = message.From.FirstName,
            LastName = message.From.LastName,
            Username = message.From.Username,
            Messages = new List<string>
            {
                "assistant:Привет! Это бот для общения с ChatGPT. \nЗадай любой вопрос и я постараюсь на него ответить!"
            }
        };
        CreateUser(user);
        return user;
    }
    
    public void CreateUser(UserModel user)
    {

        Users.Add(user.Id, user);
        _applicationContext.Users.Add(user);
        _applicationContext.SaveChanges();
        
    }

    public void DeleteUser(long id)
    {
        Users.Remove(id);
        var dbUser = _applicationContext.Users.FirstOrDefault(u => u.Id == id);
        _applicationContext.Users.Remove(dbUser);
        _applicationContext.SaveChanges();
    }

    private void FillUsersDict()
    {
        Users = new Dictionary<long, UserModel>();
        foreach (var user in _applicationContext.Users)
        {
            Users.Add(user.Id, user);
        }
    }
}