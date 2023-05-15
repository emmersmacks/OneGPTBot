using ChatBotGPT.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatBotGPT.Database;

public class ApplicationContext : DbContext
{
    public List<UserModel> Users { get; set; }
    public List<AccessModel> Accesses { get; set; }

    public ApplicationContext()
    {
        Database.EnsureCreated();
        CreateTables();
    }

    private async void CreateTables()
    {
        Users = new List<UserModel>();
        Accesses = new List<AccessModel>();
        await SaveChangesAsync();
        Console.WriteLine("AAAA");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(@"Host=91.199.147.114;Database=Users;Username=DbUser;Password=Dbiyz123"); 
    }
}