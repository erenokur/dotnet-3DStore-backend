using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using dotnet_3D_store_backend.Models;
using dotnet_3D_store_backend.Contexts;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();
builder.Services.AddSingleton<AppSettings>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(appSettings.ApiCorsPolicy,
    builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {

        var secretKey = Encoding.UTF8.GetBytes(appSettings.Secret);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = false,
            ValidIssuer = appSettings.BearerValidIssuer,
            ValidAudience = appSettings.BearerValidAudience,
            IssuerSigningKey = new SymmetricSecurityKey(secretKey)
        };
    });

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseMySQL(appSettings.ConnectionString));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
    options.AddPolicy("SellerPolicy", policy => policy.RequireRole("Seller"));
    options.AddPolicy("UserOrSellerPolicy", policy =>
  {
      policy.RequireAssertion(context =>
          context.User.HasClaim(ClaimTypes.Role, "User") ||
          context.User.HasClaim(ClaimTypes.Role, "Seller"));
  });
});

builder.Services.AddSignalR();

var app = builder.Build();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors(appSettings.ApiCorsPolicy);

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<NotificationContext>(appSettings.NotificationSocket);
});

app.Run();
