using Application.DTO.Administration;

namespace Application.Requests.Administration.Roles;

public record AddMentorRequest(string Username, Guid CourseId);