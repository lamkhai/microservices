using System.ComponentModel.DataAnnotations;

namespace CoreAPI.Data.Models.Requests;
#nullable disable

public class UserRequest
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}