namespace dotnet_3D_store_backend.Models;

using System.ComponentModel.DataAnnotations;

public class AddProductRequest
{
    [Required]
    public string Name { get; set; } = String.Empty;

    [Required]
    public string Description { get; set; } = String.Empty;

    [Required]
    public string Category { get; set; } = String.Empty;

    public IFormFile Image { get; set; } = null!;

    [Required]
    public string Currency { get; set; } = String.Empty;
    [Required]
    public decimal Price { get; set; }

    [Required]
    public int Quantity { get; set; }

    public int SellerUserId { get; set; }

}