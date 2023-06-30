namespace dotnet_3D_store_backend.DTOs;

using System.ComponentModel.DataAnnotations;

public class AuthenticateRequest
{
    [Required]
    public string Email { get; set; } = String.Empty;

    [Required]
    public string Password { get; set; } = String.Empty;
}