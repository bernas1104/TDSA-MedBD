using System.Collections.Generic;
using Bogus;
using TDSA_MedBDAPI.ViewModels;
using TDSA_MedBDDomain.Models;

namespace TDSA_MedBDTest.Bogus.ViewModels {
  public static class DoctorViewModelFaker {
    public static DoctorViewModel Generate(Doctor doctor) {
      var viewModel = new Faker<DoctorViewModel>()
        .RuleFor(x => x.Fullname, () => doctor.Fullname)
        .RuleFor(x => x.DoctorSpecialties, (f) => f.Random.WordsArray(1, 5))
        .RuleFor(x => x.Crm, () => doctor.Crm)
        .RuleFor(x => x.Cpf, () => doctor.Cpf);

      return viewModel.Generate();
    }

    public static IList<DoctorViewModel> Generate(int quantity) {
      var viewModel = new Faker<DoctorViewModel>()
        .RuleFor(x => x.Fullname, (f) => f.Person.FirstName)
        .RuleFor(x => x.DoctorSpecialties, (f) => f.Random.WordsArray(1, 5))
        .RuleFor(x => x.Crm, () => "crm")
        .RuleFor(x => x.Cpf, () => "cpf");

      return viewModel.Generate(quantity);
    }
  }
}
