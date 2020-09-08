using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TDSA_MedBDAPI.Services.Interfaces;
using TDSA_MedBDAPI.ViewModels;
using TDSA_MedBDDomain.Interfaces.Repositories;

namespace TDSA_MedBDAPI.Controllers {
  [ApiController]
  [Route("medico")]
  [Produces("application/json")]
  public class DoctorsController : ControllerBase {
    [HttpGet]
    public ActionResult<IList<DoctorViewModel>> Index(
      [FromServices] IDoctorServices services
    ) {
      var doctors = services.ListDoctors();

      if (doctors.Count == 0)
        return NoContent();

      return Ok(doctors);
    }

    [HttpPost]
    public ActionResult<int> Create(
      [FromServices] IDoctorServices services,
      [FromBody] CreateDoctorViewModel doctor
    ) {
      var doctor_id = services.RegisterDoctor(doctor);
      return Ok(doctor_id);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public ActionResult Delete([FromServices] IDoctorServices services, int id) {
      var deletedDoctor = services.DeleteDoctor(id);
      return NoContent();
    }
  }
}
