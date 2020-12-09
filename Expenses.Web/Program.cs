using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Expenses.Web
{
  public class Program
  {
    public async static Task Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();

      using (var scope = host.Services.CreateScope())
      {
        var servicesProvider = scope.ServiceProvider;

        var logger = servicesProvider.GetRequiredService<ILogger<Program>>();
        try
        {
          logger.LogInformation("Running migrations...");

          var dbContext = servicesProvider.GetRequiredService<Business.Data.ExpensesDbContext>();
          await dbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
          logger.LogError(ex, "Initialization");

          throw;
        }

        logger.LogInformation("Spawning host...");
        await host.RunAsync();
      }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}
