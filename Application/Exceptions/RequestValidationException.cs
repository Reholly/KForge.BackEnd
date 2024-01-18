using FluentValidation.Results;

namespace Application.Exceptions;

public class RequestValidationException(ValidationFailure validationFailure) 
    : ApplicationLayerException(validationFailure.ErrorMessage, 400);