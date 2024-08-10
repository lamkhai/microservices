using System.ComponentModel.DataAnnotations;

namespace CoreAPI.Data.Models.Requests;

public class CreateProductRequest
{
    [Required]
    public string Name { get; set; }

    public decimal Price { get; set; }
}