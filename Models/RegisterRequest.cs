namespace dotnet_3D_store_backend.Models;

using System.ComponentModel.DataAnnotations;

public class RegisterRequest
{
    [Required]
    public string Email { get; set; } = String.Empty;

    [Required]
    public string Password { get; set; } = String.Empty;

    [Required]
    public string UserName { get; set; } = String.Empty;
    [Required]
    public string Role { get; set; } = String.Empty;
}