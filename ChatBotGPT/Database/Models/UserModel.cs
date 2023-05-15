using System.ComponentModel.DataAnnotations;

namespace ChatBotGPT.Database.Models;

public class UserModel
{
    [Key]
    public long Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<string> Messages { get; set; }
}