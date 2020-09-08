using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TDSA_MedBDAPI.Controllers;
using TDSA_MedBDAPI.Services.Interfaces;
using TDSA_MedBDAPI.ViewModels;
using TDSA_MedBDTest.Bogus.ViewModels;
using Xunit;

namespace TDSA_MedBDTest.UnitTests.Controllers {
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

    [Fact]
    public void Should_Return_No_Content_If_No_Doctors_Are_Registered() {
      doctorServices.Setup(x => x.ListDoctors()).Returns(
        new List<DoctorViewModel>()
      );

      var response = doctorsController.Index(doctorServices.Object);

      var actionResult = Assert.IsType<NoContentResult>(response.Result);

      Assert.NotNull(actionResult);
      Assert.Equal(StatusCodes.Status204NoContent, actionResult.StatusCode);
    }

    [Fact]
    public void Should_Return_List_Of_All_Registered_Doctors() {
      var rnd = new Random();
      var rndNumber = rnd.Next(1, 11);

      IList<DoctorViewModel> doctorViewModels = new List<DoctorViewModel>();
      for(var i = 0; i < rndNumber; i++)
        doctorViewModels.Add(new DoctorViewModel());

      doctorServices.Setup(x => x.ListDoctors()).Returns(doctorViewModels);

      var response = doctorsController.Index(doctorServices.Object);

      var actionResult = Assert.IsType<OkObjectResult>(response.Result);
      var actionValue = Assert.IsType<List<DoctorViewModel>>(actionResult.Value);

      Assert.NotNull(actionResult);
      Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);
      Assert.Equal(rndNumber, actionValue.ToList().Count);
    }
  }
}
