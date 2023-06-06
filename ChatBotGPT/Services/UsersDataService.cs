using ChatBotGPT.Database;
using ChatBotGPT.Database.Models;
using Telegram.Bot.Types;

namespace VideoBot.Services;

public class UsersDataService
{
    public Dictionary<long, UserModel> Users;

    public UsersDataService()
    {
        FillUsersDict();
    }

    public void UpdateUser(UserModel userModel)
    {
        var user = Users[userModel.Id];
        if(user == null)
            throw new Exception($"{nameof(this.GetType)}: -20 | User for update not found!");

        Users[userModel.Id] = userModel;
        using (var context = new ApplicationContext())
        {
            context.Users.Update(userModel);
            context.SaveChanges();
        }
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
        using (var context = new ApplicationContext())
        {
            Users.Add(user.Id, user);
            context.Users.Add(user);
            context.SaveChanges();
        }
    }

    public void DeleteUser(long id)
    {
        Users.Remove(id);
        using (var context = new ApplicationContext())
        {
            var dbUser = context.Users.FirstOrDefault(u => u.Id == id);
            context.Users.Remove(dbUser);
            context.SaveChanges();
        }
    }

    private void FillUsersDict()
    {
        Users = new Dictionary<long, UserModel>();
        using (var context = new ApplicationContext())
        {
            foreach (var user in context.Users)
            {
                Users.Add(user.Id, user);
            }
        }
    }
}