namespace dotnet_3D_store_backend.DTOs;

using System.ComponentModel.DataAnnotations;

public class ProductsBySearchRequest
{
    [Required]
    public string Search { get; set; } = String.Empty;
    [Required]
    public int CurrentPage { get; set; }
}