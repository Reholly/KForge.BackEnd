using Application.DTO.Edu;
using Domain.Entities;

namespace Application.Models;

public record CreateTestTaskModel
{
    public required TestTaskDto TaskDto { get; init; }
    public required ApplicationUser Author { get; init; }
    public required Course Course { get; init; }
}