using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using TDSA_MedBDAPI.Exceptions;

namespace TDSA_MedBDAPI.Middlewares {
  public class ExceptionMiddleware : IMiddleware {
    private readonly IWebHostEnvironment environment;
    private readonly string ContentType = "application/json";

    public ExceptionMiddleware(IWebHostEnvironment environment) {
      this.environment = environment;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
      try {
        await next(context);
      } catch (AppException de) {
        var json = HandleAppException(context, de);
        await HandleResponse(context, json);
      } catch (Exception ex) {
        var json = HandleUnknownException(context, ex);
        await HandleResponse(context, json);
      }
    }

    private string HandleAppException(HttpContext context, AppException exception) {
      int statusCode = exception.StatusCode;

      var json = JsonConvert.SerializeObject(new {
        status = statusCode,
        title = exception.Message,
        errors = exception.Errors,
      });

      context.Response.StatusCode = statusCode;

      return json;
    }

    private string HandleUnknownException(HttpContext context, Exception exception) {
      var json = JsonConvert.SerializeObject(new {
        status = 500,
        title = "Erro desconhecido",
        error = environment.IsDevelopment() ? exception : null
      });

      context.Response.StatusCode = 500;

      return json;
    }

    private Task HandleResponse(HttpContext context, string json) {
      context.Response.ContentType = ContentType;

      return context.Response.WriteAsync(json);
    }
  }
}
