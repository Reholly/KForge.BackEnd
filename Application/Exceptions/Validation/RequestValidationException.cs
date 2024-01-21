using FluentValidation.Results;
using ApplicationException = Application.Exceptions.Common.ApplicationException;

namespace Application.Exceptions.Validation;

public class RequestValidationException(ValidationFailure validationFailure) 
    : ApplicationException(validationFailure.ErrorMessage, 400);