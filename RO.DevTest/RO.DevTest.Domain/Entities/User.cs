using Microsoft.AspNetCore.Identity;

namespace RO.DevTest.Domain.Entities;

public class User : IdentityUser
{
    public string Name { get; set; } = string.Empty; // Nome completo do usuário
    public string Role { get; set; } = "User"; // Ex.: "Admin" ou "User"
}
