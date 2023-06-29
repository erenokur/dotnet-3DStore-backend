namespace dotnet_3D_store_backend.Models;

using System.ComponentModel.DataAnnotations;
using dotnet_3D_store_backend.Enumerators;

public class RegisterRequest
{
    [Required]
    public string Email { get; set; } = String.Empty;

    [Required]
    public string Password { get; set; } = String.Empty;

    [Required]
    public string UserName { get; set; } = String.Empty;
    [Required]
    public UserRoles Role { get; set; } = UserRoles.Guest;
}