using System;
using FluentValidation;
using TDSA_MedBDAPI.ViewModels;

namespace TDSA_MedBDAPI.Validators {
  public class CreateDoctorViewModelValidator
    : AbstractValidator<CreateDoctorViewModel> {
    public CreateDoctorViewModelValidator() {
      RuleFor(x => x.Fullname)
        .NotEmpty()
        .WithMessage("nome é obrigatório")
        .NotNull()
        .WithMessage("nome é obrigatório")
        .MaximumLength(255)
        .WithMessage("nome tem no máximo 255 caracteres")
        .WithName("nome")
        .OverridePropertyName("nome");

      RuleFor(x => x.Cpf)
        .NotEmpty()
        .WithMessage("cpf é obrigatório")
        .Length(11)
        .WithMessage("cpf inválido")
        .Matches("[0-9]{11}")
        .WithMessage("cpf deve conter apenas números")
        .Must(ValidateCpf)
        .WithMessage("cpf inválido")
        .WithName("cpf")
        .OverridePropertyName("cpf");

      RuleFor(x => x.Crm)
        .NotEmpty()
        .WithMessage("crm é obrigatório")
        .NotNull()
        .WithMessage("crm é obrigatório")
        .MinimumLength(4)
        .WithMessage("crm tem no mínimo 4 caracteres")
        .Matches(
          "^[0-9]*[1-9]{1}-(AC|AL|AP|AM|BA|CE|DF|ES|GO|MA|MT|MS|MG|PA|PB|PR|PE|PI|RJ|RN|RS|RO|RR|SC|SP|SE|TO)$"
        )
        .WithMessage("crm inválido")
        .WithName("crm")
        .OverridePropertyName("crm");

      RuleFor(x => x.Specialties)
        .NotNull()
        .WithMessage("médico deve ter pelo menos uma especialidade")
        .NotEmpty()
        .WithMessage("médico deve ter pelo menos uma especialidade")
        .WithName("especialidades")
        .OverridePropertyName("especialidades");
    }

    private bool ValidateCpf(string cpf) {
      if (cpf == null) return false;

      var firstDigit = NthDigitValidation(cpf, 1);
      var secondDigit = NthDigitValidation(cpf, 2);

      return (Int32.Parse(new string(cpf[9], 1)) != firstDigit)
        || Int32.Parse(new string(cpf[10], 1)) != secondDigit;
    }

    private int NthDigitValidation(string cpf, int digit) {
      int weight = 12 - digit - 1;

      var sum = 0;
      var aux = 0;

      for(var i = 0;; i++) {
        sum += Int32.Parse(new string(cpf[i], 1)) * (weight - aux);
        aux++;

        if (aux == 11 - digit) break;
      }

      var validationDigit = 11 - (sum % 11);
      if (validationDigit > 9) return 0;

      return validationDigit;
    }
  }
}
