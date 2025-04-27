using RO.DevTest.Domain.Entities;

public class CreateUserResult
{
    public string Id { get; set; } = string.Empty; // Inicializado com valor padrão
    public string UserName { get; set; } = string.Empty; // Inicializado com valor padrão
    public string Email { get; set; } = string.Empty; // Inicializado com valor padrão
    public string Name { get; set; } = string.Empty; // Inicializado com valor padrão

    public CreateUserResult(User user)
    {
        Id = user.Id?.ToString() ?? string.Empty; // Prevenção de referência nula
        UserName = user.UserName ?? string.Empty; // Prevenção de referência nula
        Email = user.Email ?? string.Empty; // Prevenção de referência nula
        Name = user.Name ?? string.Empty; // Prevenção de referência nula
    }
}
