using System;
using Bogus;
using TDSA_MedBDDomain.Models;

namespace TDSA_MedBDTest.Bogus.Models {
  public static class SpecialtyFaker {
    public static Specialty Generate(string name) {
      var specialty = new Faker<Specialty>()
        .RuleFor(x => x.Id, (f) => f.Random.Int(1, 100))
        .RuleFor(x => x.Name, () => name)
        .RuleFor(x => x.CreatedAt, () => DateTime.Now)
        .RuleFor(x => x.DeletedAt, (_, u) => u.CreatedAt);

      return specialty.Generate();
    }
  }
}
