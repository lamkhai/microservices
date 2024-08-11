using CoreAPI.Data.DBContext;
using CoreAPI.Data.Entities;
using CoreAPI.Data.Models.Requests;
using CoreAPI.Data.Models.Responses;
using CoreAPI.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;

namespace CoreAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly CoreAPIDBContext _DBContext;

    public UserController(CoreAPIDBContext DBContext)
    {
        _DBContext = DBContext;
    }

    [HttpGet]
    public async Task<IActionResult> Get(int page = 1, int pageSize = 10)
    {
        var result = await _DBContext.Users.AsNoTracking()
                                           .Skip((page - 1) * pageSize)
                                           .Take(pageSize)
                                           .Select(x => new UserResponse(x))
                                           .ToListAsync();
        return Ok(result);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
    {
        var user = await _DBContext.Users.Select(x => new
        {
            x.Id,
            x.UserName,
            x.HashedPassword,
            x.UpdatedDate,
            x.Roles,
        })
        .FirstOrDefaultAsync(x => x.UserName == request.UserName && x.HashedPassword == request.Password.Hash());

        if (user == null)
        {
            return Unauthorized();
        }

        var claims = new List<Claim>
        {
            new("Id", user.Id.ToString()),
            new("UserName", user.UserName),
            new("UpdatedDate", user.UpdatedDate.ToString()),
            new(ClaimTypes.Role, JsonSerializer.Serialize(user.Roles)),
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = request.RememberMe,
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties
            );

        return Ok();
    }

    [HttpGet("Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok();
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] UserRequest request)
    {
        var user = new User
        {
            UserName = request.UserName,
            HashedPassword = request.Password.Hash(),
        };

        await _DBContext.AddAsync(user);

        await _DBContext.SaveChangesAsync();

        return Ok(new UserResponse(user));
    }
}