using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Web.Api.Controllers;

[ApiController]
public class ApiController : ControllerBase {

  protected IActionResult Problem(List<Error> errors) {
    if (errors.Count == 0) {
      return Problem();
    }

    // Si todos los errores son de validación
    if (errors.All(error => error.Type == ErrorType.Validation)) {
      var modelState = new ModelStateDictionary();

      // Convertir errores en ModelState
      foreach (var error in errors) {
        modelState.AddModelError(error.Code, error.Description);
      }

      return ValidationProblem(
          modelState); // Devuelve los errores como ValidationProblem
    }

    HttpContext.Items[Errors.HttpContextItemKeys.Errors] = errors;

    return Problem(errors[0]);
  }

  private IActionResult Problem(Error error) {
    var statusCode = error.Type switch {
      ErrorType.Conflict =>
          StatusCodes.Status409Conflict, // Corregido: usar StatusCodes
      ErrorType.Validation => StatusCodes.Status400BadRequest,
      ErrorType.NotFound => StatusCodes.Status404NotFound,
      _ => StatusCodes
               .Status500InternalServerError // Código de estado predeterminado
    };

    return Problem(statusCode: statusCode, title: error.Description,
                   detail: error.Code);
  }
}
