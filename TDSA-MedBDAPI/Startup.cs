using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TDSA_MedBDAPI.Configurations;
using TDSA_MedBDAPI.Middlewares;
using TDSA_MedBDInfra.Context;

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

      services
        .AddMvc()
        .AddNewtonsoftJson(
          opt => opt.SerializerSettings.ReferenceLoopHandling =
            Newtonsoft.Json.ReferenceLoopHandling.Ignore
          )
        .AddFluentValidation(
          opt => {
            opt.RegisterValidatorsFromAssemblyContaining<Startup>();
          }
        );

      services.ResolveDependencies();
      services.AddAutoMapper(typeof(Startup));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();

      app.UseMiddleware<ExceptionMiddleware>();

      app.UseEndpoints(endpoints => {
        endpoints.MapControllers();
      });
    }
  }
}
