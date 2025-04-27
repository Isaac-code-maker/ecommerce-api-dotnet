using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RO.DevTest.Domain.Entities;
using RO.DevTest.Persistence;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using RO.DevTest.Application.Contracts.Persistence.Repositories;
using RO.DevTest.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configurar o banco de dados
builder.Services.AddDbContext<DefaultContext>(options =>
    options.UseInMemoryDatabase("DefaultDatabase")); // UseInMemoryDatabase para testes

// Configurar Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DefaultContext>()
    .AddDefaultTokenProviders();

// Configurar autenticação JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is not configured.")))
        };
    });

// Adicionar serviços de controladores
builder.Services.AddControllers();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
