using Microsoft.OpenApi.Models;
using project7.Services;
using WebApplication2.Interfaces;
using WebApplication2.Services;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder.Services);

        var app = builder.Build();

        Configure(app, builder.Environment);

        app.Run();
    }

    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
        });

        services.AddScoped<IAuthorsService, AuthorsService>();
        services.AddSingleton<IBooksService, BooksService>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddSingleton<IUserEncryptionService, UserEncryptionService>();

        services.AddScoped<IUserService>(serviceProvider =>
        {
            var userEncryptionService = serviceProvider.GetRequiredService<IUserEncryptionService>();

            string jwtSecret = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9"; 
            int jwtExpirationInMinutes = 60; 

            return new UserService(jwtSecret, jwtExpirationInMinutes);
        });
    }

    public static void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1");
            });
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
}



//https://localhost:7264/swagger