using Microsoft.EntityFrameworkCore;
using RO.DevTest.Application.Contracts.Persistence.Repositories;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Persistence.Repositories;

/// <summary>
/// Implementation of customer repository
/// </summary>
public class CustomerRepository : ICustomerRepository {
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context) {
        _context = context;
    }

    public async Task<Customer?> GetByIdAsync(Guid id) {
        return await _context.Customers.FindAsync(id);
    }

    public async Task<IEnumerable<Customer>> GetAllAsync() {
        return await _context.Customers.ToListAsync();
    }

    public async Task AddAsync(Customer customer) {
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Customer customer) {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id) {
        var customer = await GetByIdAsync(id);
        if (customer != null) {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}