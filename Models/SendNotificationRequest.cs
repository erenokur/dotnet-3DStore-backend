namespace dotnet_3D_store_backend.Models;

using System.ComponentModel.DataAnnotations;

public class SendNotificationRequest
{

    [Required]
    public string Message { get; set; } = String.Empty;

}