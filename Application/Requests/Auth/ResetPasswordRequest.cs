namespace Application.Requests.Auth;

public record ResetPasswordRequest(
    string Username,
    string OldPassword, 
    string NewPassword,
    string RepeatedNewPassword);