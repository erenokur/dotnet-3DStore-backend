namespace dotnet_3D_store_backend.Models;

using System.ComponentModel.DataAnnotations;
using dotnet_3D_store_backend.Enumerators;

public class UpdateUserRequest
{
    public string Password { get; set; } = String.Empty;

    public string UserName { get; set; } = String.Empty;

    public int UserId { get; set; }
}