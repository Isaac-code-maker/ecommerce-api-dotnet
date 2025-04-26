namespace RO.DevTest.Domain.Entities;

/// <summary>
/// Represents a product in the e-commerce system.
/// </summary>
public class Product {
    public Guid Id { get; set; } = Guid.NewGuid(); // Unique identifier
    public string Name { get; set; } = string.Empty; // Product's name
    public decimal Price { get; set; } // Product's price
    public int Stock { get; set; } // Quantity in stock
}