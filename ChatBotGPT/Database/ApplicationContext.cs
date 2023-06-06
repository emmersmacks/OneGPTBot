using ChatBotGPT.Database.Models;
using Microsoft.EntityFrameworkCore;
using VideoBot.Data.SSH;

namespace ChatBotGPT.Database;

public class ApplicationContext : DbContext//, IDisposable
{
    public DbSet<UserModel> Users { get; set; } = null!;
    public DbSet<AccessModel> Accesses { get; set; } = null!;

    private SshTunnel _sshTunnel;

    public ApplicationContext()
    {
        var tunnelConfig = new TunnelConfig
        {
            Host = "91.199.147.114",
            Port = 22,
            Username = "root",
            Password = "aZ4hJ9mZ5qoD",
        };
        //_sshTunnel = new SshTunnel(tunnelConfig);
        
        //_sshTunnel.Start();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(@"Host=91.199.147.114;Database=Users;Username=DbUser;Password=Dbiyz123"); 
    }

    //public void Dispose()
    //{
    //    //_sshTunnel.Dispose();
    //}
}