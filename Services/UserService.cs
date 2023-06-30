namespace dotnet_3D_store_backend.Services;

using Microsoft.Extensions.Options;
using dotnet_3D_store_backend.Models;
using dotnet_3D_store_backend.Helpers;
using dotnet_3D_store_backend.Entities;
using dotnet_3D_store_backend.Contexts;
using dotnet_3D_store_backend.DTOs;

public class UserService
{
    private readonly DatabaseContext _dbContext;
    private readonly JwtHelper _jwtHelper;
    public UserService(IOptions<AppSettings> appSettings, DatabaseContext dbContext)
    {
        _dbContext = dbContext;
        _jwtHelper = new JwtHelper(appSettings);
    }

    public AuthenticateResponse? Authenticate(AuthenticateRequest model)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.Email == model.Email);
        if (user == null) return null;

        bool result = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
        if (!result) return null;

        // authentication successful so generate jwt token
        var tokenJson = _jwtHelper.GenerateJwtToken(user);

        // serialize user and token to JSON strings
        string userJson = System.Text.Json.JsonSerializer.Serialize(user);

        AuthenticateResponse response = new()
        {
            Id = user.Id,
            UserName = user.UserName,
            AccessToken = tokenJson
        };
        return response;
    }
    public bool Register(RegisterRequest model)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.Email == model.Email);
        if (user != null) return false;
        // hash password
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
        // create user
        Users newUser = new()
        {
            Email = model.Email,
            Password = hashedPassword,
            UserName = model.UserName,
            Created = DateTime.Now,
            Role = model.Role
        };

        // insert user
        _dbContext.Users.Add(newUser);
        _dbContext.SaveChanges();
        return true;
    }
    public bool UpdateUser(UpdateUserRequest model)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.Id == model.UserId);
        if (user == null) return false;
        if (!string.IsNullOrEmpty(model.Password))
            user.Password = model.Password;
        if (!string.IsNullOrEmpty(model.UserName))
            user.UserName = model.UserName;
        _dbContext.SaveChanges();
        return true;
    }

    public bool ChangeUserRoleRequest(ChangeUserRoleRequest model)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.Id == model.UserId);
        if (user == null) return false;
        user.Role = model.Role;
        _dbContext.SaveChanges();
        return true;
    }

    public IEnumerable<Users> GetAll()
    {
        return _dbContext.Users
            .Select(u => new Users
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                Created = u.Created,
                Role = u.Role
            })
            .ToList();
    }

    public Users? GetById(int id)
    {
        return _dbContext.Users.FirstOrDefault(x => x.Id == id);
    }
}