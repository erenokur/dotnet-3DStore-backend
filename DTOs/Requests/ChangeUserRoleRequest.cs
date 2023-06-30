namespace dotnet_3D_store_backend.DTOs;

using System.ComponentModel.DataAnnotations;
using dotnet_3D_store_backend.Enumerators;

public class ChangeUserRoleRequest
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public UserRoles Role { get; set; } = UserRoles.Guest;
}