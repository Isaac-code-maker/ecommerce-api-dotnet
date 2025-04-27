namespace RO.DevTest.Domain.Entities;

/// <summary>
/// Represents a customer in the e-commerce system.
/// </summary>
public class Customer
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}