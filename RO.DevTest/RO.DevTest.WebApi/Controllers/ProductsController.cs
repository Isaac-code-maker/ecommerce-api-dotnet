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
    public async Task<IActionResult> GetAll() {
        var products = await _productRepository.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id) {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Product product) {
        await _productRepository.AddAsync(product);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Product product) {
        if (id != product.Id) return BadRequest();
        await _productRepository.UpdateAsync(product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) {
        await _productRepository.DeleteAsync(id);
        return NoContent();
    }
}