using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Ocelot ke liye configuration file load kar rahe hain
            builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

            // Ocelot Middleware Add Karna
            builder.Services.AddOcelot();

            // Swagger aur Controllers Add Karna
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Development Mode ke liye Swagger Enable Karna
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapControllers();

            // Ocelot Middleware Lagana (Important!)
            app.UseOcelot().Wait();

            app.Run();
        }
    }
}
