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

      dbContext.AddRange(
        new Specialty {
          Id = 2,
          Name = "Dermatologista"
        },
        new Specialty {
          Id = 3,
          Name = "Psiquiatra"
        }
      );

      for(var i = 1; i <= doctorCpfs.Count; i++) {
        var doctor = new Doctor {
          Id = i,
          Fullname = "John Doe",
          Cpf = doctorCpfs[i-1],
          Crm = doctorCrms[i-1],
          DoctorSpecialties = new List<DoctorSpecialty>() {
            new DoctorSpecialty {
              DoctorId = i,
              SpecialtyId = i % 3 != 0 ? (i % 3) : 3,
            }
          }
        };

        if (i == 9) doctor.DeletedAt = DateTime.Now;

        dbContext.Doctors.Add(doctor);
      }

      dbContext.SaveChanges();
    }
  }
}
