using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation.Results;
using TDSA_MedBDAPI.Exceptions;
using TDSA_MedBDAPI.Services.Interfaces;
using TDSA_MedBDAPI.ViewModels;
using TDSA_MedBDDomain.Interfaces.Repositories;
using TDSA_MedBDDomain.Models;
using TDSA_MedBDDomain.Validations.DoctorValidations;

namespace TDSA_MedBDAPI.Services {
  public class DoctorServices : IDoctorServices {
    private readonly IDoctorsRepository doctorsRepository;
    private readonly ISpecialtiesRepository specialtiesRepository;
    private readonly IMapper mapper;

    public DoctorServices(
      IDoctorsRepository doctorsRepository,
      ISpecialtiesRepository specialtiesRepository,
      IMapper mapper
    ) {
      this.doctorsRepository = doctorsRepository;
      this.specialtiesRepository = specialtiesRepository;
      this.mapper = mapper;
    }

    public IList<DoctorViewModel> ListDoctors() {
      return mapper.Map<IList<DoctorViewModel>>(doctorsRepository.FindAll());
    }

    public IList<DoctorViewModel> ListDoctorsBySpecialty(string specialty) {
      throw new NotImplementedException();
    }

    public int RegisterDoctor(CreateDoctorViewModel doctor) {
      var model = mapper.Map<Doctor>(doctor);
      model.DoctorSpecialties = new List<DoctorSpecialty>();

      var validation = new DoctorInsertValidation(doctorsRepository)
        .Validate(model);

      if (!validation.IsValid)
        HandleModelValidationError(validation.Errors);

      doctorsRepository.Add(model);

      IList<Specialty> specialties = new List<Specialty>();
      foreach(var specialty in doctor.Specialties) {
        var addingSpecialty = specialtiesRepository.FindByName(specialty);

        if (addingSpecialty == null)
          throw new AppException("Especialidade não encontrada", 404, null);

        model.DoctorSpecialties.Add(new DoctorSpecialty {
          Specialty = addingSpecialty,
          SpecialtyId = addingSpecialty.Id,
          Doctor = model,
          DoctorId = model.Id
        });
      }

      doctorsRepository.Save();

      return model.Id;
    }

    public int DeleteDoctor(int id) {
      var doctor = doctorsRepository.FindById(id);
      if (doctor == null)
        throw new AppException("Médico não cadastrado", 404, null);

      if (doctor.DeletedAt != null)
        throw new AppException("Médico já foi deletado", 400, null);

      doctor.DeletedAt = DateTime.Now;

      doctorsRepository.Update(doctor);
      doctorsRepository.Save();

      return doctor.Id;
    }

    private void HandleModelValidationError(IList<ValidationFailure> errors) {
      IList<dynamic> exceptionErrors = new List<dynamic>();

      foreach(var error in errors) {
        exceptionErrors.Add(new {
          error.PropertyName,
          error.ErrorMessage,
        });
      }

      throw new AppException("Erro ao cadastrar médico", 400, exceptionErrors);
    }
  }
}
