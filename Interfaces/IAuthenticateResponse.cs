namespace dotnet_3D_store_backend.interfaces;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using dotnet_3D_store_backend.Entities;
using dotnet_3D_store_backend.Helpers;
using dotnet_3D_store_backend.Models;

public interface IAuthenticateResponse
{
    int Id { get; set; }
    string UserName { get; set; }
    string User { get; set; }
    string AccessToken { get; set; }
    string Message { get; set; }
}
