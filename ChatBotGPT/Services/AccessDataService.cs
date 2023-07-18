using ChatBotGPT.Database;
using ChatBotGPT.Database.Models;

namespace VideoBot.Services;

public class AccessDataService
{
    private readonly ApplicationContext _applicationContext;
    public Dictionary<long, AccessModel> Accesses;

    public AccessDataService(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
        FillAccessDict();
    }

    public void AddAccess(long id)
    {
        var newAccess = new AccessModel()
        {
            Id = id
        };
        
        Accesses.Add(id, newAccess);
        _applicationContext.Accesses.Add(newAccess);
        _applicationContext.SaveChanges();
    }

    public void RemoveAccess(long id)
    {
        Accesses.Remove(id);
        var access = _applicationContext.Accesses.FirstOrDefault(a => a.Id == id);
        _applicationContext.Accesses.Remove(access);
        _applicationContext.SaveChanges();
    }

    public bool UserIsAvailable(long id)
    {
        if (UserIsAdministrator(id))
            return true;
        
        return Accesses.ContainsKey(id);
    }
    
    public bool UserIsAdministrator(long id)
    {
        return id == 1419158298;
    }
    
    private void FillAccessDict()
    {
        Accesses = new Dictionary<long, AccessModel>();
        foreach (var access in _applicationContext.Accesses)
        {
            Accesses.Add(access.Id, access);
        }
    }
}