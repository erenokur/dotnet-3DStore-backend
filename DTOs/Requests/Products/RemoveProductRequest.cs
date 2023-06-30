namespace dotnet_3D_store_backend.DTOs;

using System.ComponentModel.DataAnnotations;

public class RemoveProductRequest
{
    [Required]
    public int ProductId { get; set; }
}