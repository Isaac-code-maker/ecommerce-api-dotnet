using RO.DevTest.Application.Contracts.Persistence.Repositories;
using RO.DevTest.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Registro dos repositórios
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();

var app = builder.Build();
