using FluentValidation;
using TDSAMedBDAPI.ViewModels;

namespace TDSAMedBDAPI.Validators {
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
        .WithMessage("cpf inválido")
        .NotNull()
        .WithMessage("cpf inválido")
        .Length(11)
        .WithMessage("cpf inválido")
        .Matches("[0-9]{11}")
        .WithMessage("cpf deve conter apenas números")
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
  }
}
