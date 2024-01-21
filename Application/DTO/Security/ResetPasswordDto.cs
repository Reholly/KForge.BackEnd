namespace Application.DTO.Security;

public record ResetPasswordDto(
    string Username,
    string OldPassword, 
    string NewPassword,
    string RepeatedNewPassword);