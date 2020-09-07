using Microsoft.AspNetCore.Mvc;
using TDSAMedBDAPI.Services.Interfaces;
using TDSAMedBDAPI.ViewModels;

namespace TDSAMedBDAPI.Controllers {
  [ApiController]
  [Route("medico")]
  [Produces("application/json")]
  public class DoctorsController : ControllerBase {
    [HttpPost]
    public ActionResult<int> Create(
      [FromServices] IDoctorServices services,
      [FromBody] DoctorViewModel doctor
    ) {
      // TODO
      return Ok();
    }
  }
}
