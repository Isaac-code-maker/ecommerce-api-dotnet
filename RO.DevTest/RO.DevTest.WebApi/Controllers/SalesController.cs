using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> GetAll(
        [FromQuery] Guid? customerId,
        [FromQuery] Guid? productId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? sort = "date",
        [FromQuery] string? order = "asc")
    {
        var query = (await _saleRepository.GetAllAsync()).AsQueryable();

        // Filtragem por cliente
        if (customerId.HasValue)
        {
            query = query.Where(s => s.CustomerId == customerId.Value);
        }

        // Filtragem por produto
        if (productId.HasValue)
        {
            query = query.Where(s => s.ProductId == productId.Value);
        }

        // Filtragem por intervalo de datas
        if (startDate.HasValue)
        {
            query = query.Where(s => s.Date >= startDate.Value);
        }
        if (endDate.HasValue)
        {
            query = query.Where(s => s.Date <= endDate.Value);
        }

        // Ordenação
        query = sort.ToLower() switch
        {
            "date" => order.ToLower() == "desc" ? query.OrderByDescending(s => s.Date) : query.OrderBy(s => s.Date),
            "totalprice" => order.ToLower() == "desc" ? query.OrderByDescending(s => s.TotalPrice) : query.OrderBy(s => s.TotalPrice),
            _ => query
        };

        // Paginação
        var totalItems = query.Count();
        var sales = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return Ok(new
        {
            TotalItems = totalItems,
            Page = page,
            PageSize = pageSize,
            Data = sales
        });
    }

    [Authorize]
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

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Sale updatedSale)
    {
        var sale = await _saleRepository.GetByIdAsync(id);
        if (sale == null)
        {
            return NotFound("Venda não encontrada.");
        }

        // Atualizar os campos da venda
        sale.Quantity = updatedSale.Quantity > 0 ? updatedSale.Quantity : sale.Quantity;
        sale.TotalPrice = updatedSale.TotalPrice > 0 ? updatedSale.TotalPrice : sale.TotalPrice;

        await _saleRepository.UpdateAsync(sale);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var sale = await _saleRepository.GetByIdAsync(id);
        if (sale == null)
        {
            return NotFound("Venda não encontrada.");
        }

        await _saleRepository.DeleteAsync(id);
        return NoContent();
    }
}