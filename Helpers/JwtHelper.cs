namespace dotnet_3D_store_backend.Helpers;

using dotnet_3D_store_backend.Entities;
using dotnet_3D_store_backend.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


public class JwtHelper
{
    private readonly AppSettings _appSettings;
    public JwtHelper(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public string generateJwtToken(Users user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim("username", user.UserName),
                // Add more claims as needed
            };
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }


}
