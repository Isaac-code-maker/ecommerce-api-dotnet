using Microsoft.EntityFrameworkCore;
using RO.DevTest.Application.Contracts.Persistence.Repositories;
using RO.DevTest.Persistence;
using RO.DevTest.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configurar o Entity Framework com um banco de dados em memória
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("EcommerceDb")); // Nome do banco de dados em memória

// Registrar os repositórios
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();

// Adicionar serviços de controladores
builder.Services.AddControllers();

var app = builder.Build();

// Configurar os endpoints
app.MapControllers();

app.Run();
