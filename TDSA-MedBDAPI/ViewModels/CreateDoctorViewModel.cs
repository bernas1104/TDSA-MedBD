using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TDSA_MedBDAPI.ViewModels {
  public class CreateDoctorViewModel {
    public string Fullname { get; set; }
    public string Cpf { get; set; }
    public string Crm { get; set; }
    public ICollection<string> Specialties { get; set; }
  }
}
