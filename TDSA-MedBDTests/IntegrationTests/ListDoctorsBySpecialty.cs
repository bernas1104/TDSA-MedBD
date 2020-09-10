using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TDSA_MedBDAPI;
using TDSA_MedBDTest.IntegrationTests.DTOs;
using Xunit;

namespace TDSA_MedBDTest.IntegrationTests {
  public class ListDoctorsBySpecialtyTest
  : IClassFixture<CustomWebApplicationFactory<Startup>> {
    private readonly CustomWebApplicationFactory<Startup> factory;
    private readonly HttpClient Client;

    public ListDoctorsBySpecialtyTest(CustomWebApplicationFactory<Startup> factory) {
      this.factory = factory;
      Client = this.factory.CreateClient();
    }

    [Fact]
    public async Task Should_Return_200_Status_Code_If_Doctors_Of_Specialty_Are_Found() {
      var response = await factory.PerformRequest(new PerformRequestDTO {
        Client = Client,
        HttpAction = 1,
        HttpRoute = "medico/dermatologista",
      });

      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_204_Status_Code_If_No_Doctors_Of_Specialty_Are_Found() {
      var response = await factory.PerformRequest(new PerformRequestDTO {
        Client = Client,
        HttpAction = 1,
        HttpRoute = "medico/Ginecologista",
      });

      Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_404_Status_Code_If_Specialty_Does_Not_Exist() {
      var response = await factory.PerformRequest(new PerformRequestDTO {
        Client = Client,
        HttpAction = 1,
        HttpRoute = "medico/Lorem",
      });

      Console.WriteLine(await response.Content.ReadAsStringAsync());

      Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
  }
}
