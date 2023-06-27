namespace dotnet_todo_backend.Controllers;


using System.Security.Claims;
using dotnet_3D_store_backend.Helpers;
using dotnet_3D_store_backend.interfaces;
using dotnet_3D_store_backend.Models;
using dotnet_3D_store_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;


[ApiController]
[Route("[controller]")]

public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private ProductService _productService;
    private AppSettings _appSettings;

    public ProductController(ILogger<ProductController> logger, IOptions<AppSettings> appSettings, DatabaseContext dbContext)
    {
        _productService = new ProductService(dbContext);
        _logger = logger;
        _appSettings = appSettings.Value;
    }

    [Authorize]
    [HttpGet("getSuggestedProducts")]
    public IActionResult GetSuggestedProducts()
    {
        var userIdString = HttpContext.User.FindFirst("id")?.Value;
        if (userIdString == null)
        {
            return BadRequest("User ID not found.");
        }
        var userId = int.Parse(userIdString);
        var products = _productService.GetSuggestedProducts(userId);
        return Ok(products);
    }

}