namespace TDSAMedBDDomain.Models {
  public class DoctorSpecialty {
    public string DoctorId { get; set; }
    public string SpecialtyId { get; set; }

    public Doctor Doctor { get; set; }
    public Specialty Specialty { get; set; }
  }
}
