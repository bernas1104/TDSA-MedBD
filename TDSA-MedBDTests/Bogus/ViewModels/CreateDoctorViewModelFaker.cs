using Bogus;
using TDSA_MedBDAPI.ViewModels;

namespace TDSA_MedBDTest.Bogus.ViewModels {
  public static class CreateDoctorViewModelFaker {
    public static CreateDoctorViewModel Generate(string crm, string cpf) {
      var viewModel = new Faker<CreateDoctorViewModel>()
        .RuleFor(x => x.Fullname, (f) => f.Name.FullName())
        .RuleFor(x => x.Specialties, (f) => f.Random.WordsArray(1, 5))
        .RuleFor(x => x.Crm, () => crm)
        .RuleFor(x => x.Cpf, () => cpf);

      return viewModel.Generate();
    }
  }
}
