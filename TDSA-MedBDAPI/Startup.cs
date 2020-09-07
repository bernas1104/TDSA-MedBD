using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TDSAMedBDDomain.Context;

namespace TDSA_MedBDAPI {
  public class Startup {
    public IConfiguration Configuration { get; set; }
    public IWebHostEnvironment Environment { get; set; }

    public Startup(IConfiguration configuration, IWebHostEnvironment environment) {
      Configuration = configuration;
      Environment = environment;
    }

    public void ConfigureServices(IServiceCollection services) {
      services.AddDbContext<TDSAMedBDContext>(
        opt => {
          if (Environment.IsDevelopment())
            opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
          if (Environment.IsProduction())
            opt.UseNpgsql(Configuration["DATABASE_URL"]);
        }
      );
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();

      app.UseEndpoints(endpoints => {
        endpoints.MapControllers();
      });
    }
  }
}
