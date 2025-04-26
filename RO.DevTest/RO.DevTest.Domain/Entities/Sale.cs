namespace RO.DevTest.Domain.Entities;

public class Sale
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }

    // Adicionando a propriedade Date
    public DateTime Date { get; set; } = DateTime.UtcNow; // Define a data atual como padrão
}