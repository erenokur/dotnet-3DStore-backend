namespace dotnet_3D_store_backend.Models;

using System.ComponentModel.DataAnnotations;

public class RemoveProductRequest
{
    [Required]
    public int ProductId { get; set; }
}