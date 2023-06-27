namespace dotnet_3D_store_backend.interfaces;
public interface IRegisterResponse
{
    int Id { get; set; }
    string UserName { get; set; }
    string Email { get; set; }
    string AccessToken { get; set; }
}