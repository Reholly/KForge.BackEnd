using Microsoft.AspNetCore.Routing;

namespace Application.Models;

public record RequestParametersModel(RouteValueDictionary RouteParameters);