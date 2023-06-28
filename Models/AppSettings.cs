
namespace dotnet_3D_store_backend.Models;
public class AppSettings
{
    public string Secret { get; set; } = String.Empty;
    public string ConnectionString { get; set; } = String.Empty;
    public string DatabaseName { get; set; } = String.Empty;
    public int PageSize { get; set; }
    public string ImageServerLocalPath { get; set; } = String.Empty;

}