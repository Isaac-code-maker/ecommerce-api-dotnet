using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Application.Contracts.Persistence.Repositories;

/// <summary>
/// Interface for customer repository
/// </summary>
public interface ICustomerRepository {
    Task<Customer?> GetByIdAsync(Guid id);
    Task<IEnumerable<Customer>> GetAllAsync();
    Task AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(Guid id);
}