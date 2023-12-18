using Lab_3_Mini_API.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab_3_Mini_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("ApplicationContext");

            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
