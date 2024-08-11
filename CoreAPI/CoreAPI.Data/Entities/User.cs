using System.ComponentModel.DataAnnotations;

namespace CoreAPI.Data.Entities;
#nullable disable

public sealed class User
{
    public Guid Id { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    public string HashedPassword { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public List<Role> Roles { get; set; }
    public List<UserRole> UserRoles { get; set; }
}