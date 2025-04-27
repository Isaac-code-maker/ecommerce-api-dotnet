using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Application.Contracts.Persistence.Repositories;

/// <summary>
/// Interface for sale repository
/// </summary>
public interface ISaleRepository {
    Task<Sale?> GetByIdAsync(Guid id);
    Task AddAsync(Sale sale);
    Task UpdateAsync(Sale sale);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Sale>> GetAllAsync();
}