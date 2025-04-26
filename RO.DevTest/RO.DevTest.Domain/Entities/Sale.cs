namespace RO.DevTest.Domain.Entities;

/// <summary>
/// Represents a sale in the e-commerce system.
/// </summary>
public class Sale {
    public Guid Id { get; set; } = Guid.NewGuid(); // Unique identifier
    public Guid CustomerId { get; set; } // Reference to the customer
    public Guid ProductId { get; set; } // Reference to the product
    public int Quantity { get; set; } // Quantity sold
    public decimal TotalPrice { get; set; } // Total price of the sale
    public DateTime SaleDate { get; set; } = DateTime.UtcNow; // Date of the sale
}