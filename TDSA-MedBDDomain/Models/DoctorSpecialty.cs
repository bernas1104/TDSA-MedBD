namespace TDSA_MedBDDomain.Models {
  public class DoctorSpecialty {
    public int DoctorId { get; set; }
    public int SpecialtyId { get; set; }

    public Doctor Doctor { get; set; }
    public Specialty Specialty { get; set; }
  }
}
