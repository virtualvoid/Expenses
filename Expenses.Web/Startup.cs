using AutoMapper;
using Expenses.Web.Business.Data;
using Expenses.Web.Business.Mediator;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Expenses.Web
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services
        .AddAuthentication(
          c =>
          {
            c.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            c.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            c.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
          }
        )
        .AddCookie(
          c =>
          {
            c.LoginPath = "/User/Login";
            c.LogoutPath = "/User/Logout";
          }
        );

      services.Configure<CookiePolicyOptions>(
        c =>
        {
          c.CheckConsentNeeded = context => false;
          c.MinimumSameSitePolicy = SameSiteMode.None;
        }
      );

      var connectionString = Configuration.GetConnectionString("default");
      services.AddDbContext<ExpensesDbContext>(
        opts => opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
      );

      services.AddAutoMapper(typeof(Startup));
      services.AddMediatR(RequestHandlers.Collection, opts => opts.AsTransient());

      services.AddControllersWithViews()
        .AddRazorRuntimeCompilation();

      //CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
      //CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Default/Error");
        app.UseHsts();
      }
      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseForwardedHeaders(
        new ForwardedHeadersOptions
        {
          ForwardedHeaders = ForwardedHeaders.All
        }
      );

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Default}/{action=Index}/{id?}");
      });
    }
  }
}
