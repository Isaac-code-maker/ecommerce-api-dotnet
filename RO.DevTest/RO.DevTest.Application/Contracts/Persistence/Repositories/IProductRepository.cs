using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Application.Contracts.Persistence.Repositories;

/// <summary>
/// Interface for product repository
/// </summary>
public interface IProductRepository {
    Task<Product?> GetByIdAsync(Guid id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Guid id);
}