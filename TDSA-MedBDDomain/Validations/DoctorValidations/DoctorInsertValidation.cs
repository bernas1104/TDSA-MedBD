using System;
using FluentValidation;
using TDSA_MedBDDomain.Interfaces.Repositories;
using TDSA_MedBDDomain.Models;

namespace TDSA_MedBDDomain.Validations.DoctorValidations {
  public class DoctorInsertValidation : AbstractValidator<Doctor> {
    private readonly IDoctorsRepository doctorsRepository;

    public DoctorInsertValidation(IDoctorsRepository doctorsRepository) {
      this.doctorsRepository = doctorsRepository;

      RuleFor(x => x)
        .Must(ValidateUniqueCpf)
        .WithMessage("cpf já utilizado")
        .WithName("cpf")
        .OverridePropertyName("cpf");

      RuleFor(x => x)
        .Must(ValidateUniqueCrm)
        .WithMessage("crm já utilizado")
        .WithName("crm")
        .OverridePropertyName("crm");
    }

    private bool ValidateUniqueCpf(Doctor doctor) {
      return doctorsRepository.FindByCpf(doctor.Cpf) == null;
    }

    private bool ValidateUniqueCrm(Doctor doctor) {
      return doctorsRepository.FindByCrm(doctor.Crm) == null;
    }
  }
}
