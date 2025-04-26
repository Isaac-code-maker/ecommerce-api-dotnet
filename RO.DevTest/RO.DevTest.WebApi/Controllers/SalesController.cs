using Microsoft.AspNetCore.Mvc;
using RO.DevTest.Application.Contracts.Persistence.Repositories;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase {
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;

    public SalesController(ISaleRepository saleRepository, IProductRepository productRepository) {
        _saleRepository = saleRepository;
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() {
        var sales = await _saleRepository.GetAllAsync();
        return Ok(sales);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Sale sale) {
        var product = await _productRepository.GetByIdAsync(sale.ProductId);
        if (product == null || product.Stock < sale.Quantity) {
            return BadRequest("Produto não disponível ou estoque insuficiente.");
        }

        sale.TotalPrice = product.Price * sale.Quantity;
        product.Stock -= sale.Quantity;

        await _productRepository.UpdateAsync(product);
        await _saleRepository.AddAsync(sale);

        return CreatedAtAction(nameof(GetAll), new { id = sale.Id }, sale);
    }
}