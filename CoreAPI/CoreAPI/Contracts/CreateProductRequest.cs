using System.ComponentModel.DataAnnotations;

namespace CoreAPI.Contracts;

public class CreateProductRequest
{
    [Required]
    public string Name { get; set; }

    public decimal Price { get; set; }
}