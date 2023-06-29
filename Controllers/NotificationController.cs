namespace dotnet_todo_backend.Controllers;


using dotnet_3D_store_backend.Contexts;
using dotnet_3D_store_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

[ApiController]
[Route("[controller]")]

public class NotificationController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private IHubContext<NotificationContext> _notificationContext;

    public NotificationController(ILogger<AuthController> logger, IHubContext<NotificationContext> hubContext)
    {
        _logger = logger;
        _notificationContext = hubContext;
    }

    [Authorize(Policy = "AdminPolicy")]
    [HttpPost("sendNotificationToAll")]
    public IActionResult sendNotificationToAll(SendNotificationRequest model)
    {
        _notificationContext.Clients.All.SendAsync("ReceiveNotification", model.Message);
        return Ok();
    }

}