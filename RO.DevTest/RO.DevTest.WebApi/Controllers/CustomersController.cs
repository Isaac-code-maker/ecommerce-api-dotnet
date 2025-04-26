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
    public async Task<IActionResult> GetAll() {
        var customers = await _customerRepository.GetAllAsync();
        return Ok(customers);
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