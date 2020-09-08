using Microsoft.AspNetCore.Mvc;
using TDSA_MedBDAPI.Services.Interfaces;
using TDSA_MedBDAPI.ViewModels;

namespace TDSA_MedBDAPI.Controllers {
  [ApiController]
  [Route("medico")]
  [Produces("application/json")]
  public class DoctorsController : ControllerBase {
    [HttpPost]
    public ActionResult<int> Create(
      [FromServices] IDoctorServices services,
      [FromBody] CreateDoctorViewModel doctor
    ) {
      var doctor_id = services.RegisterDoctor(doctor);
      return Ok(doctor_id);
    }
  }
}
