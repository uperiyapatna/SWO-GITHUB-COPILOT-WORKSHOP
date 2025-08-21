using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BikeShopAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHealthChecks().AddCheck("AppCheck", () =>
            {
                bool isSystemOK = CheckPerformance();

                if (isSystemOK)

                {
                    return HealthCheckResult.Healthy("The application is operating.");
                }
                else
                {
                    bool minorIssue = CheckIfMinorIssue();

                    return minorIssue ?
                        HealthCheckResult.Degraded("The application is operating with minor issues but remains stable.") :
                        HealthCheckResult.Unhealthy("There is a system failure with the application, which requires immediate attention.");
                }

                bool CheckPerformance()
                {
                    Random random = new Random();
                    int randomNumber = random.Next(1, 50);

                    return randomNumber > 5;
                }

                bool CheckIfMinorIssue()
                {
                    Random random = new Random();
                    int randomNumber = random.Next(1, 50);

                    return randomNumber > 25;
                }
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    }
                );
            });

            var app = builder.Build();

            app.MapHealthChecks("/api/health");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAll");
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}