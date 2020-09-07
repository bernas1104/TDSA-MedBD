using System.Collections.Generic;

namespace TDSAMedBDAPI.ViewModels {
  public class DoctorViewModel {
    public string Fullname { get; set; }
    public string Cpf { get; set; }
    public string Crm { get; set; }
    public ICollection<string> Specialties { get; set; }
  }
}
