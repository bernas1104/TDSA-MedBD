using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TDSA_MedBDAPI;
using TDSA_MedBDAPI.ViewModels;
using TDSA_MedBDTest.Bogus.ViewModels;
using TDSA_MedBDTest.IntegrationTests.DTOs;
using Xunit;

namespace TDSA_MedBDTest.IntegrationTests {
  public class RegisterDoctorTest
  : IClassFixture<CustomWebApplicationFactory<Startup>> {
    private readonly CustomWebApplicationFactory<Startup> factory;

    public RegisterDoctorTest(CustomWebApplicationFactory<Startup> factory) {
      this.factory = factory;
    }

    [Fact]
    public async Task Should_Return_200_Status_Code_When_Doctor_Registered() {
      var client = factory.CreateClient();

      var data = CreateDoctorViewModelFaker.Generate("1-DF", "98531289009");
      data.Specialties = new List<string>() { "Clínico Geral" };

      var response = await factory.PerformRequest(new PerformRequestDTO {
        Client = client,
        Data = data,
        HttpAction = 2,
        HttpRoute = "medico"
      });

      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_400_Status_Code_When_Invalid_ViewModel() {
      var client = factory.CreateClient();

      var data = new CreateDoctorViewModel();

      var response = await factory.PerformRequest(new PerformRequestDTO {
        Client = client,
        Data = data,
        HttpAction = 2,
        HttpRoute = "medico"
      });

      Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_400_Status_Code_If_CPF_or_CRM_Not_Unique() {
      var client = factory.CreateClient();

      var data = CreateDoctorViewModelFaker.Generate("2-DF", "61567855075");
      data.Specialties = new List<string>() { "Clínico Geral" };

      var response = await factory.PerformRequest(new PerformRequestDTO {
        Client = client,
        Data = data,
        HttpAction = 2,
        HttpRoute = "medico"
      });

      Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Should_Return_404_Status_Code_If_Any_Specialty_Does_Not_Exist() {
      var client = factory.CreateClient();

      var data = CreateDoctorViewModelFaker.Generate("3-DF", "48315415085");
      data.Specialties = new List<string>() { "Lorem Ipsum" };

      var response = await factory.PerformRequest(new PerformRequestDTO {
        Client = client,
        Data = data,
        HttpAction = 2,
        HttpRoute = "medico"
      });

      Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
  }
}
