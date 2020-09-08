using Bogus;
using TDSA_MedBDAPI.ViewModels;
using TDSA_MedBDDomain.Models;

namespace TDSA_MedBDTest.Bogus.Models {
  public static class DoctorFaker {
    public static Doctor Generate(CreateDoctorViewModel data) {
      var doctor = new Faker<Doctor>()
        .RuleFor(x => x.Fullname, () => data.Fullname)
        .RuleFor(x => x.Cpf, () => data.Cpf)
        .RuleFor(x => x.Crm, () => data.Crm);

      return doctor.Generate();
    }

    public static Doctor Generate(string cpf, string crm) {
      var doctor = new Faker<Doctor>()
        .RuleFor(x => x.Fullname, (f) => f.Person.FullName)
        .RuleFor(x => x.Cpf, () => cpf)
        .RuleFor(x => x.Crm, () => crm);

      return doctor.Generate();
    }
  }
}
