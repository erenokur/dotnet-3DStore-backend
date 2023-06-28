namespace dotnet_3D_store_backend.Models;

using System.ComponentModel.DataAnnotations;

public class SuggestedProductsRequest
{
    public int UserId { get; set; }

    [Required]
    public int CurrentPage { get; set; }

}