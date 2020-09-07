using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TDSAMedBDAPI.Controllers;
using TDSAMedBDAPI.Services.Interfaces;
using TDSAMedBDTest.Bogus.ViewModels;
using Xunit;

namespace TDSAMedBDTest.UnitTests {
  public class DoctorsControllerTest {
    private readonly Mock<IDoctorServices> doctorServices;
    private readonly DoctorsController doctorsController;

    public DoctorsControllerTest() {
      doctorServices = new Mock<IDoctorServices>();
      doctorsController = new DoctorsController();
    }

    [Fact]
    public void Should_Return_Doctor_ID_If_Doctor_Created() {
      var rnd = new Random();
      var id = rnd.Next(1, 11);

      var data = CreateDoctorViewModelFaker.Generate("1-DF", "53350519067");

      doctorServices.Setup(x => x.RegisterDoctor(data)).Returns(id);

      var response = doctorsController.Create(doctorServices.Object, data);

      var actionResult = Assert.IsType<OkObjectResult>(response.Result);
      var actionValue = Assert.IsType<int>(actionResult.Value);

      Assert.NotNull(actionResult);
      Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);
      Assert.Equal(id, actionValue);
    }
  }
}
