namespace dotnet_todo_backend.Controllers;


using System.Security.Claims;
using dotnet_3D_store_backend.Contexts;
using dotnet_3D_store_backend.interfaces;
using dotnet_3D_store_backend.Models;
using dotnet_3D_store_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using dotnet_3D_store_backend.DTOs;

[ApiController]
[Route("[controller]")]

public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private ProductService _productService;
    private AppSettings _appSettings;

    public ProductController(ILogger<ProductController> logger, IOptions<AppSettings> appSettings, DatabaseContext dbContext)
    {
        _appSettings = appSettings.Value;
        _productService = new ProductService(dbContext, _appSettings);
        _logger = logger;

    }

    [Authorize(Policy = "SellerPolicy")]
    [HttpPost("addProduct")]
    public IActionResult AddProduct(AddProductRequest request)
    {
        var userId = Convert.ToInt32(HttpContext.User.FindFirst("id")?.Value);
        if (userId == 0)
        {
            return BadRequest();
        }
        request.SellerUserId = userId;
        var result = _productService.AddProduct(request);
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }

    [Authorize(Policy = "SellerPolicy")]
    [HttpPost("removeProduct")]
    public IActionResult RemoveProduct(RemoveProductRequest request)
    {
        var result = _productService.DeleteProduct(request);
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpGet("getSuggestedProducts")]
    public IActionResult GetSuggestedProducts(SuggestedProductsRequest request)
    {
        var products = _productService.GetSuggestedProductsAsync(request);
        return Ok(products);
    }

    [HttpGet("GetProductsByCategory")]
    public IActionResult GetProductsByCategory(ProductsByCategoryRequest request)
    {
        var products = _productService.GetProductsByCategory(request);
        return Ok(products);
    }

    [HttpGet("GetProductsBySearch")]
    public IActionResult GetProductsBySearch(ProductsBySearchRequest request)
    {
        var products = _productService.GetProductsBySearchAsync(request);
        return Ok(products);
    }
}