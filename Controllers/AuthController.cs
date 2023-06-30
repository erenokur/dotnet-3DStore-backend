namespace dotnet_todo_backend.Controllers;


using System.Security.Claims;
using dotnet_3D_store_backend.Contexts;
using dotnet_3D_store_backend.interfaces;
using dotnet_3D_store_backend.Models;
using dotnet_3D_store_backend.DTOs;
using dotnet_3D_store_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;


[ApiController]
[Route("[controller]")]

public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private UserService _userService;
    private AppSettings _appSettings;


    public AuthController(ILogger<AuthController> logger, IOptions<AppSettings> appSettings, DatabaseContext dbContext)
    {
        _userService = new UserService(appSettings, dbContext);
        _logger = logger;
        _appSettings = appSettings.Value;
    }

    [HttpPost("login")]
    public IActionResult Login(AuthenticateRequest request)
    {
        var response = _userService.Authenticate(request);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var response = _userService.Register(request);
        if (response == false)
            return BadRequest(new { message = "Register Failed" });

        return Ok(new { message = "User registered successfully!" });
    }

    [Authorize(Policy = "AdminPolicy")]
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }

    [Authorize]
    [HttpGet("getUserData")]
    public IActionResult GetUserData()
    {
        var userId = HttpContext.User.FindFirst("id")?.Value;
        var userName = HttpContext.User.FindFirst("username")?.Value;
        if (!string.IsNullOrEmpty(userName))
        {
            return Ok(new { username = userName, userId = userId });
        }
        else
        {
            return BadRequest(new { message = "Username or password is not found" });
        }

    }

    [Authorize(Policy = "AdminPolicy")]
    [HttpPost("changeUserRole")]
    public IActionResult changeUserRole(ChangeUserRoleRequest request)
    {
        var result = _userService.ChangeUserRoleRequest(request);
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }

    [Authorize(Policy = "UserOrSellerPolicy")]
    [HttpPost("updateUser")]
    public IActionResult UpdateUser(UpdateUserRequest request)
    {
        var userId = HttpContext.User.FindFirst("id")?.Value;
        request.UserId = Convert.ToInt32(userId);
        var result = _userService.UpdateUser(request);
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }
}