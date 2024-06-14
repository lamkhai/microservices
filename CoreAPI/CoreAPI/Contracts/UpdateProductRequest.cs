using System.ComponentModel.DataAnnotations;

namespace CoreAPI.Contracts;

public class UpdateProductRequest
{
    [Required]
    public string Name { get; set; }

    public decimal Price { get; set; }
}