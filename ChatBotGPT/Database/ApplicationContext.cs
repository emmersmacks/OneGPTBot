using ChatBotGPT.Database.Models;
using Microsoft.EntityFrameworkCore;
using VideoBot.Data;
using VideoBot.Data.SSH;
using VideoBot.Services;

namespace ChatBotGPT.Database;

public class ApplicationContext : DbContext
{
    private readonly ConfigService _configService;
    public DbSet<UserModel> Users { get; set; } = null!;
    public DbSet<AccessModel> Accesses { get; set; } = null!;

    private SshTunnel _sshTunnel;

    public ApplicationContext(ConfigService configService)
    {
        _configService = configService;
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configService.GetString(ConfigNames.DatabaseConnection)); 
    }
}