using TDSA_MedBDDomain.Models;
using TDSA_MedBDInfra.Context;

namespace TDSA_MedBDTest.IntegrationTests.Utilities {
  public static class DatabaseSeeder {
    public static void InitializeDatabase(TDSAMedBDContext dbContext) {
      dbContext.Doctors.Add(new Doctor {
        Fullname = "John Doe",
        Cpf = "61567855075",
        Crm = "2-DF",
      });

      dbContext.SaveChanges();
    }
  }
}
