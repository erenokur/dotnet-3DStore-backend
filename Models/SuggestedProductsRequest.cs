namespace dotnet_3D_store_backend.Models;

using System.ComponentModel.DataAnnotations;

public class SuggestedProductsRequest
{
    [Required]
    public int category { get; set; }

    [Required]
    public int currentPage { get; set; }

}