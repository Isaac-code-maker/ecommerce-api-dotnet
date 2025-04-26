using Microsoft.AspNetCore.Mvc;
using RO.DevTest.Application.Contracts.Persistence.Repositories;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase {
    private readonly ICustomerRepository _customerRepository;

    public CustomersController(ICustomerRepository customerRepository) {
        _customerRepository = customerRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? name, // Filtragem por nome
        [FromQuery] int page = 1, // Número da página
        [FromQuery] int pageSize = 10, // Tamanho da página
        [FromQuery] string? sort = "name", // Campo para ordenação
        [FromQuery] string? order = "asc") // Ordem (ascendente ou descendente)
    {
        // Obter todos os clientes
        var query = (await _customerRepository.GetAllAsync()).AsQueryable();

        // Filtragem por nome
        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        // Ordenação
        query = sort.ToLower() switch
        {
            "name" => order.ToLower() == "desc" ? query.OrderByDescending(c => c.Name) : query.OrderBy(c => c.Name),
            "email" => order.ToLower() == "desc" ? query.OrderByDescending(c => c.Email) : query.OrderBy(c => c.Email),
            _ => query // Sem ordenação adicional
        };

        // Paginação
        var totalItems = query.Count();
        var customers = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        // Retornar os resultados com metadados
        return Ok(new
        {
            TotalItems = totalItems,
            Page = page,
            PageSize = pageSize,
            Data = customers
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id) {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer == null) return NotFound();
        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Customer customer) {
        await _customerRepository.AddAsync(customer);
        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Customer customer) {
        if (id != customer.Id) return BadRequest();
        await _customerRepository.UpdateAsync(customer);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) {
        await _customerRepository.DeleteAsync(id);
        return NoContent();
    }
}