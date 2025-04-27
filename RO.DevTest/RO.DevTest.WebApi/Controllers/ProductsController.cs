using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RO.DevTest.Application.Contracts.Persistence.Repositories;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase {
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository) {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? name, // Filtragem por nome
        [FromQuery] decimal? minPrice, // Filtragem por preço mínimo
        [FromQuery] decimal? maxPrice, // Filtragem por preço máximo
        [FromQuery] int page = 1, // Número da página
        [FromQuery] int pageSize = 10, // Tamanho da página
        [FromQuery] string? sort = "name", // Campo para ordenação
        [FromQuery] string? order = "asc") // Ordem (ascendente ou descendente)
    {
        // Obter todos os produtos
        var query = (await _productRepository.GetAllAsync()).AsQueryable();

        // Filtragem por nome
        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        // Filtragem por faixa de preço
        if (minPrice.HasValue)
        {
            query = query.Where(p => p.Price >= minPrice.Value);
        }
        if (maxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= maxPrice.Value);
        }

        // Ordenação
        query = sort.ToLower() switch
        {
            "name" => order.ToLower() == "desc" ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
            "price" => order.ToLower() == "desc" ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
            _ => query // Sem ordenação adicional
        };

        // Paginação
        var totalItems = query.Count();
        var products = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        // Retornar os resultados com metadados
        return Ok(new
        {
            TotalItems = totalItems,
            Page = page,
            PageSize = pageSize,
            Data = products
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound("Produto não encontrado.");
        }

        return Ok(product);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _productRepository.AddAsync(product);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Product updatedProduct)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound("Produto não encontrado.");
        }

        // Agora é seguro acessar as propriedades de 'product'
        product.Name = updatedProduct.Name ?? product.Name;
        product.Price = updatedProduct.Price > 0 ? updatedProduct.Price : product.Price;

        await _productRepository.UpdateAsync(product);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) {
        await _productRepository.DeleteAsync(id);
        return NoContent();
    }
}