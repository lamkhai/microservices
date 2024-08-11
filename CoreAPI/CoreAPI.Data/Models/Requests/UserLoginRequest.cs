namespace CoreAPI.Data.Models.Requests;

public class UserLoginRequest : UserRequest
{
    public bool RememberMe { get; set; }
}