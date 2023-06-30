namespace dotnet_3D_store_backend.DTOs;

using System.ComponentModel.DataAnnotations;

public class ProductsByCategoryRequest
{
    [Required]
    public string Category { get; set; } = String.Empty;
    [Required]
    public int CurrentPage { get; set; }
}