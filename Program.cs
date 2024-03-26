using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using project7.Services;
using WebApplication2.Interfaces;
using WebApplication2.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "WebAPI", Version = "v1" });
});

builder.Services.AddScoped<IAuthorsService, AuthorsService>();
builder.Services.AddSingleton<IBooksService, BooksService>();
builder.Services.AddTransient<IOrderService, OrderService>();

builder.Services.AddSingleton<IUserEncryptionService, UserEncryptionService>();

builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
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

app.Run();


//https://localhost:7264/swagger