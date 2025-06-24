
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Web.Data;
using Web.Repository;

namespace Web
{
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
            builder.Services.AddScoped<IContactRepository, ContactRepository>();

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
                .WithHeaders(HeaderNames.ContentType)
                //.AllowAnyOrigin()
                );

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
