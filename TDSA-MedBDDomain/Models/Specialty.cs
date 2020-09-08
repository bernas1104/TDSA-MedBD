using System;
using System.Collections.Generic;

namespace TDSA_MedBDDomain.Models {
  public class Specialty {
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public virtual IList<DoctorSpecialty> DoctorSpecialties { get; set; }
  }
}
