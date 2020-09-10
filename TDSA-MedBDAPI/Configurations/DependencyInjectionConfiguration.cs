using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TDSA_MedBDAPI.Middlewares;
using TDSA_MedBDAPI.Services;
using TDSA_MedBDAPI.Services.Interfaces;
using TDSA_MedBDDomain.Interfaces.Repositories;
using TDSA_MedBDInfra.Repositories;

namespace TDSA_MedBDAPI.Configurations {
  public static class DependencyInjection {
    public static IServiceCollection ResolveDependencies(
      this IServiceCollection services
    ) {
      services.AddScoped<IDoctorsRepository, DoctorsRepository>();
      services.AddScoped<ISpecialtiesRepository, SpecialtiesRepository>();
      services.AddScoped<IDoctorSpecialtiesRepository, DoctorSpecialtiesRepository>();

      services.AddTransient<IDoctorServices, DoctorServices>();

      services.AddTransient<ExceptionMiddleware>();

      return services;
    }

    // public static void UseExceptionMiddleware(this IApplicationBuilder app) {
    //   app.UseMiddleware<ExceptionMiddleware>();
    // }
  }
}
