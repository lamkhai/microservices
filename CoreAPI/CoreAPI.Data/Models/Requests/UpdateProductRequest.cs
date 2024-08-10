using System.ComponentModel.DataAnnotations;

namespace CoreAPI.Data.Models.Requests;

public class UpdateProductRequest
{
    [Required]
    public string Name { get; set; }

    public decimal Price { get; set; }
}