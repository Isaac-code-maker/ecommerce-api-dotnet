namespace RO.DevTest.Domain.Entities;

public class Sale
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime Date { get; set; } // Adicionada a propriedade Date
}