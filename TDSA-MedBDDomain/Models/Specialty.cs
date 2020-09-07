using System;
using System.Collections.Generic;

namespace TDSAMedBDDomain.Models {
  public class Specialty {
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public ICollection<DoctorSpecialty> Doctors { get; set; }
  }
}
