using System;
using System.Collections.Generic;
using TDSA_MedBDDomain.Models;
using TDSA_MedBDInfra.Context;

namespace TDSA_MedBDTest.IntegrationTests.Utilities {
  public static class DatabaseSeeder {
    public static void InitializeDatabase(TDSAMedBDContext dbContext) {
      IList<string> doctorCpfs = new List<string>() {
        "78909727098", "44243813078", "63567321056", "50538173084", "27618141002",
        "40402304004", "91773462032", "61161308016", "33558953002", "69404339024"
      };

      IList<string> doctorCrms = new List<string>() {
        "2-DF", "3-DF", "4-DF", "5-DF","6-DF",
        "7-DF", "8-DF", "9-DF", "10-DF", "11-DF"
      };

      IList<string> specialtiesIds = new List<string>() {
        Guid.NewGuid().ToString(),
        Guid.NewGuid().ToString()
      };

      dbContext.AddRange(
        new Specialty {
          Id = specialtiesIds[0],
          Name = "Dermatologista"
        },
        new Specialty {
          Id = specialtiesIds[1],
          Name = "Psiquiatra"
        }
      );

      for(var i = 1; i <= doctorCpfs.Count; i++) {
        var id = Guid.NewGuid().ToString();
        dbContext.Doctors.Add(new Doctor {
          Id = id,
          Fullname = "John Doe",
          Cpf = doctorCpfs[i-1],
          Crm = doctorCrms[i-1],
          DoctorSpecialties = new List<DoctorSpecialty>() {
            new DoctorSpecialty {
              DoctorId = id,
              SpecialtyId = specialtiesIds[i % 2],
            }
          }
        });
      }

      dbContext.SaveChanges();
    }
  }
}
