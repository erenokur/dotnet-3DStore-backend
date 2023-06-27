namespace dotnet_3D_store_backend.Models;

using dotnet_3D_store_backend.interfaces;
public class RegisterResponse : IRegisterResponse
{
    public int Id { get; set; }
    public string UserName { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string AccessToken { get; set; } = String.Empty;
}