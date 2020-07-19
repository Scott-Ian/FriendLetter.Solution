using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FriendLetter
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    public void ConfigureServices (IServiceCollection services)
    {
      // New!
      services.AddControllers();
      services.AddRazorPages();

      // Old
      //services.AddMvc();

    }

    public void Configure (IApplicationBuilder app)
    {
    app.UseStaticFiles();

    // Runs matching. An endpoint is selected and set on the HttpContext if a match is found.
    app.UseRouting(); 

    // Middleware that run after routing occurs. Usually the following appear here:
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseCors();
    // These middleware can take different actions based on the endpoint.

    // Executes the endpoint that was selected by routing.
    app.UseEndpoints(endpoints =>
    {
        // Mapping of endpoints goes here:
        endpoints.MapControllers();
        endpoints.MapRazorPages();
    });

    // Middleware here will only run if nothing was matched.

    //Old stuff!
      // app.UseMvc(routes =>
      // {
      //   routes.MapRoute(
      //     name: "default",
      //     template: "{controller=Home}/{action=Index}{id?}");
      // });

      app.Run(async (context) =>
      {
        await context.Response.WriteAsync("Hello World!");
      });
    }
  }
}
