using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TDSA_MedBDAPI;
using TDSA_MedBDTest.IntegrationTests.DTOs;
using Xunit;

namespace TDSA_MedBDTest.IntegrationTests {
  public class DeleteDoctorTest
  : IClassFixture<CustomWebApplicationFactory<Startup>> {
    private readonly CustomWebApplicationFactory<Startup> factory;
    private readonly HttpClient Client;

    public DeleteDoctorTest(CustomWebApplicationFactory<Startup> factory) {
      this.factory = factory;
      Client = this.factory.CreateClient();
    }

    [Fact]
    public async Task Should_Return_Status_Code_204_If_Doctor_Deleted() {
      var response = await factory.PerformRequest(new PerformRequestDTO {
        Client = Client,
        HttpAction = 5,
        HttpRoute = "medico/10",
        Data = null,
      });

      Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_Status_Code_404_If_Doctor_Not_Registered() {
      var response = await factory.PerformRequest(new PerformRequestDTO {
        Client = Client,
        HttpAction = 5,
        HttpRoute = "medico/11",
        Data = null,
      });

      Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_Status_Code_400_If_Doctor_Already_Deleted() {
      var response = await factory.PerformRequest(new PerformRequestDTO {
        Client = Client,
        HttpAction = 5,
        HttpRoute = "medico/9",
        Data = null,
      });

      Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
  }
}
