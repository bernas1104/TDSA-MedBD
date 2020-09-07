using System;
using System.Collections.Generic;

namespace TDSAMedBDDomain.Models {
  public class Doctor {
    public string Id { get; set; }
    public string Fullname { get; set; }
    public string Cpf { get; set; }
    public string Crm { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public ICollection<DoctorSpecialty> Specialties { get; set; }
  }
}
