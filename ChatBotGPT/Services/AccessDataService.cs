using ChatBotGPT.Database;
using ChatBotGPT.Database.Models;

namespace VideoBot.Services;

public class AccessDataService
{
    public Dictionary<long, AccessModel> Accesses;

    public AccessDataService()
    {
        FillAccessDict();
    }

    public void AddAccess(long id)
    {
        var newAccess = new AccessModel()
        {
            Id = id
        };
        
        Accesses.Add(id, newAccess);
        using (var context = new ApplicationContext())
        {
            
            context.Accesses.Add(newAccess);
            context.SaveChanges();
        }
    }

    public void RemoveAccess(long id)
    {
        Accesses.Remove(id);
        using (var context = new ApplicationContext())
        {
            var access = context.Accesses.FirstOrDefault(a => a.Id == id);
            context.Accesses.Remove(access);
            context.SaveChanges();
        }
    }
    
    public bool UserIsAvailable(long id)
    {
        //if (id == 1419158298)
        //    return true;
        
        return Accesses.ContainsKey(id);
    }
    
    private void FillAccessDict()
    {
        Accesses = new Dictionary<long, AccessModel>();
        using (var context = new ApplicationContext())
        {
            foreach (var access in context.Accesses)
            {
                Accesses.Add(access.Id, access);
            }
        }
    }
}