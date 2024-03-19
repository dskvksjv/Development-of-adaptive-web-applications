using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using project7.Services;

namespace project7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // зроблено для створення лише одного екземпляру AuthorsService протягом одного HTTP-запиту, що забезпечує консистентність даних
                    // і уникнення конфліктів між різними HTTP-запитами
                    services.AddScoped<IAuthorsService, AuthorsService>();

                    // щоб створити єдиний екземпляр сервісу BooksService під час життєвого циклу
                    // будь-який клієнт, який викликає цей сервіс, отримує один і той самий об'єкт, шо може бути корисним, якщо потрібно
                    // забезпечити загальну доступність даних або стану серед різних компонентів
                    services.AddSingleton<IBooksService, BooksService>();

                    //кожен раз, коли запитується залежність від контейнера,
                    //буде створюватися новий екземпляр цієї залежності
                    services.AddTransient<IOrderService, OrderService>();
                });
    }
}
