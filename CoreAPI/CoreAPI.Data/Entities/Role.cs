using System.ComponentModel.DataAnnotations;

namespace CoreAPI.Data.Entities;
#nullable disable

public sealed class Role
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public List<User> Users { get; set; }
    public List<UserRole> UserRoles { get; set; }
}