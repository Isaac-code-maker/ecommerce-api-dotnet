namespace RO.DevTest.Domain.Entities;

/// <summary>
/// Represents a customer in the e-commerce system.
/// </summary>
public class Customer {
    public Guid Id { get; set; } = Guid.NewGuid(); // Unique identifier
    public string Name { get; set; } = string.Empty; // Customer's name
    public string Email { get; set; } = string.Empty; // Customer's email
    public string PhoneNumber { get; set; } = string.Empty; // Customer's phone number
}