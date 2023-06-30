namespace dotnet_3D_store_backend.Models;
public class EmailConfig
{
    public string Host { get; set; } = String.Empty;
    public int Port { get; set; }
    public string UserName { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
}