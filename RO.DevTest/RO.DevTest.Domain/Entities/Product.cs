using System.ComponentModel.DataAnnotations;

namespace RO.DevTest.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O nome do produto é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O nome do produto não pode ter mais de 100 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "O estoque não pode ser negativo.")]
    public int Stock { get; set; }
}