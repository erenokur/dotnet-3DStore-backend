namespace dotnet_todo_backend.Controllers;


using dotnet_3D_store_backend.Contexts;
using dotnet_3D_store_backend.DTOs;
using dotnet_3D_store_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]

public class EmailController : ControllerBase
{
    private readonly ILogger<EmailController> _logger;
    private readonly EmailService _emailService;

    public EmailController(ILogger<EmailController> logger, EmailService emailService)
    {
        _logger = logger;
        _emailService = emailService;
    }

    public async Task<IActionResult> SendEmail(SendEmailRequest request)
    {
        await _emailService.SendEmailAsync(request);
        return Ok("Email sent successfully.");
    }
}