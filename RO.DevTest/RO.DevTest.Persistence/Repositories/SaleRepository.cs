using Microsoft.EntityFrameworkCore;
using RO.DevTest.Application.Contracts.Persistence.Repositories;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Persistence.Repositories;

public class SaleRepository : ISaleRepository {
    private readonly ApplicationDbContext _context;

    public SaleRepository(ApplicationDbContext context) {
        _context = context;
    }

    public async Task<Sale?> GetByIdAsync(Guid id) {
        return await _context.Sales.FindAsync(id);
    }

    public async Task<IEnumerable<Sale>> GetAllAsync() {
        return await _context.Sales.ToListAsync();
    }

    public async Task AddAsync(Sale sale) {
        await _context.Sales.AddAsync(sale);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Sale sale) {
        _context.Sales.Update(sale);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id) {
        var sale = await GetByIdAsync(id);
        if (sale != null) {
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
        }
    }
}