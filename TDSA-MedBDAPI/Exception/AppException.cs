using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace TDSA_MedBDAPI.Exceptions {
  public class AppException : Exception {
    public int StatusCode { get; set; }
    public IList<dynamic> Errors { get; set; }

    public AppException(
      string message,
      int statusCode,
      IList<dynamic> errors
    ) : base(message) {
      StatusCode = statusCode;
      Errors = errors;
    }
  }
}
