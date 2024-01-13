using Application.Exceptions;
using Application.Requests.Education.Tasks;
using FluentValidation;

namespace Application.Handlers.Edu.Tasks;

public class GetTaskByIdHandler(
    IValidator<GetTaskByIdRequest> validator)
{
    public async Task HandleAsync(GetTaskByIdRequest request, CancellationToken ct = default)
    {
        var validationResult = await validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            throw new RequestValidationException(validationResult.Errors[0]);
        }
        
        //var user = await 
    }
}