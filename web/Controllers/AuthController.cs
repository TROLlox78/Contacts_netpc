using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Web.Entities;
using Web.Services;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;
    private int minimumLength = 3;
    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserDTO newUser)
    {
        var user = await authService.Register(newUser);
        if (user == null) { return BadRequest("Username taken"); }
        return Ok(user);
    }
    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserDTO request)
    {
        if (!VerifyLogin(request))
        {
            return BadRequest("Bad data");
        }

        var token = await authService.Login(request);
        if (token == null) { return BadRequest("Username or password unrecognized"); }
        return Ok(token);

    }
    [HttpGet]
    [Authorize]
    public async Task<ActionResult> greet()
    {
        return Ok("You ARE AUTHORIzed");
    }
    private bool VerifyLogin(UserDTO u)
    {
        return (u.Username.Length > minimumLength && u.Password.Length > minimumLength);
    }

}
