
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.Text;
using Web.Data;
using Web.Repository;
using Web.Services;

namespace Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        // registering database
        builder.Services.AddDbContext<ContactsDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ContactsDb")));

        // auth scheme
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration.GetValue<string>("Auth:Issuer"),
                    ValidAudience = builder.Configuration.GetValue<string>("Auth:Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Auth:Token")!)),
                };
            });

        // depenency injections
        builder.Services.AddScoped<IContactRepository, ContactRepository>();
        builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
        builder.Services.AddScoped<IAuthService, AuthService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        // fix cross-origin request block
        app.UseCors(policy =>  // origin is client address
            policy.WithOrigins("http://localhost:7113", "https://localhost:7113")
            .AllowAnyMethod()
            .WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization)
            //.AllowAnyOrigin()
            );

        app.UseHttpsRedirection();

        app.UseAuthorization();

        // here we would add antiforgery protection if it's needed

        app.MapControllers();

        app.Run();
    }
}
