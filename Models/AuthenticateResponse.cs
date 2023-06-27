namespace dotnet_3D_store_backend.Models;

using dotnet_3D_store_backend.interfaces;
public class AuthenticateResponse : IAuthenticateResponse
{
    public int Id { get; set; }
    public string UserName { get; set; } = String.Empty;
    public string User { get; set; } = String.Empty;
    public string AccessToken { get; set; } = String.Empty;
    public string Message { get; set; } = String.Empty;
}