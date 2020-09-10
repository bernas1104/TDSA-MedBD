using System.Collections.Generic;
using Bogus;
using TDSA_MedBDDomain.Models;

namespace TDSA_MedBDTest.Bogus.Models {
  public static class DoctorSpecialtyFaker {
    public static IList<DoctorSpecialty> Generate(IList<Doctor> doctors, int specialtyId) {
      var doctorSpecialty = new Faker<DoctorSpecialty>()
        .RuleFor(x => x.SpecialtyId, () => specialtyId);

      IList<DoctorSpecialty> doctorSpecialties = new List<DoctorSpecialty>();
      foreach(var doctor in doctors) {
        doctorSpecialty.RuleFor(x => x.DoctorId, () => doctor.Id);
        doctorSpecialties.Add(doctorSpecialty.Generate());
      }

      return doctorSpecialties;
    }
  }
}
