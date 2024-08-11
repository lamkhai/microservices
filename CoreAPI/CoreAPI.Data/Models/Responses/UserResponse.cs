using CoreAPI.Data.Entities;

namespace CoreAPI.Data.Models.Responses;
#nullable disable

public class UserResponse
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public List<KeyValuePair<Guid, string>> Roles { get; set; }

    public UserResponse(User user)
    {
        Id = user.Id;
        UserName = user.UserName;
        CreatedDate = user.CreatedDate;
        UpdatedDate = user.UpdatedDate;
        Roles = user.Roles?.Select(x => new KeyValuePair<Guid, string>(x.Id, x.Name)).ToList();
    }
}