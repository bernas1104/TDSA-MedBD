using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TDSA_MedBDAPI;
using TDSA_MedBDAPI.ViewModels;
using TDSA_MedBDTest.Bogus.ViewModels;
using TDSA_MedBDTest.IntegrationTests.DTOs;
using Xunit;

namespace TDSA_MedBDTest.IntegrationTests {
  public class ListDoctorsTest
  : IClassFixture<CustomWebApplicationFactory<Startup>> {
    private readonly CustomWebApplicationFactory<Startup> factory;

    public ListDoctorsTest(CustomWebApplicationFactory<Startup> factory) {
      this.factory = factory;
    }

    [Fact]
    public async Task Should_Return_200_Status_Code_If_Doctors_Listed() {
      var client = factory.CreateClient();

      var response = await factory.PerformRequest(new PerformRequestDTO {
        Client = client,
        Data = null,
        HttpAction = 1,
        HttpRoute = "medico",
      });

      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
  }
}
