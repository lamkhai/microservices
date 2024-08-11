namespace CoreAPI.Data.Entities;
#nullable disable

public sealed class UserRole
{
    public Guid RoleId { get; set; }

    public Guid UserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Role Role { get; set; }
    public User User { get; set; }
}