using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using project7.Services;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "WebAPI", Version = "v1" });
});

// зроблено для створення лише одного екземпляру AuthorsService протягом одного HTTP-запиту, що забезпечує консистентність даних
// і уникнення конфліктів між різними HTTP-запитами
builder.Services.AddScoped<IAuthorsService, AuthorsService>();
// щоб створити єдиний екземпляр сервісу BooksService під час життєвого циклу
// будь-який клієнт, який викликає цей сервіс, отримує один і той самий об'єкт, шо може бути корисним, якщо потрібно
// забезпечити загальну доступність даних або стану серед різних компонентів
builder.Services.AddSingleton<IBooksService, BooksService>();
//кожен раз, коли запитується залежність від контейнера,
//буде створюватися новий екземпляр цієї залежності
builder.Services.AddTransient<IOrderService, OrderService>();

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
