using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TDSA_MedBDInfra.Context;
using TDSA_MedBDTest.IntegrationTests.DTOs;
using TDSA_MedBDTest.IntegrationTests.Utilities;

namespace TDSA_MedBDTest.IntegrationTests {
  public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class {
    protected override void ConfigureWebHost(IWebHostBuilder builder) {
      builder.ConfigureServices(services => {
        var descriptor = services.SingleOrDefault(
          d => d.ServiceType ==
            typeof(DbContextOptions<TDSAMedBDContext>)
        );

        services.Remove(descriptor);

        services.AddDbContext<TDSAMedBDContext>(options => {
          options.UseInMemoryDatabase("InMemoryDbForTesting");
        });

        var sp = services.BuildServiceProvider();

        using (var scope = sp.CreateScope()) {
          var scopedServices = scope.ServiceProvider;
          var db = scopedServices.GetRequiredService<TDSAMedBDContext>();
          var logger = scopedServices
            .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

          db.Database.EnsureCreated();

          try {
            DatabaseSeeder.InitializeDatabase(db);
          } catch (Exception ex) {
            logger.LogError(ex, "An error has occurred seeding the " +
              "database with data. Error: {Message}", ex.Message
            );
          }
        }
      });
    }

    public async Task<HttpResponseMessage> PerformRequest(
      PerformRequestDTO request
    ) {
      StringContent json = null;
      if (request.HttpAction == 2 || request.HttpAction == 3) {
        json = new StringContent(
          JsonConvert.SerializeObject(request.Data),
          Encoding.UTF8
        ) {
          Headers = {
            ContentType = new MediaTypeHeaderValue("application/json")
          }
        };
      }

      switch(request.HttpAction) {
        case 1: // Get
          return await request.Client.GetAsync(request.HttpRoute);
        case 2: // Post
          return await request.Client.PostAsync(request.HttpRoute, json);
        case 5: // Delete
          return await request.Client.DeleteAsync(request.HttpRoute);
        default:
          throw new Exception("HttpAction não cadastrada");
      }
    }
  }
}
