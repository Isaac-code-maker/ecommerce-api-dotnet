using Microsoft.IdentityModel.Tokens; // Certifique-se de que este está correto
using System.IdentityModel.Tokens.Jwt; // Certifique-se de que este está correto

using Microsoft.AspNetCore.Mvc;
using RO.DevTest.Domain.Entities;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginDto loginDto)
    {
        // Simulação de validação de usuário (substituir por validação real)
        if (loginDto.Username == "admin" && loginDto.Password == "password")
        {
            var token = GenerateJwtToken("admin", "Admin");
            return Ok(new { Token = token });
        }

        return Unauthorized("Credenciais inválidas.");
    }

    private string GenerateJwtToken(string username, string role)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };

        var key = _configuration["Jwt:Key"];
        if (string.IsNullOrEmpty(key))
        {
            throw new InvalidOperationException("A chave JWT não está configurada.");
        }

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public class UserLoginDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
