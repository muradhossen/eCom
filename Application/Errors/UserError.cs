using Application.Common;

namespace Application.Errors;

public static class UserError
{
    public static readonly Error NotFound = new Error(
    "User.UserNotFound", "The requested user was not found!");

    public static Error UpdateFailed(string username) => new Error(
        "User.UpdateFailed", $"Failed to update user {username}");

    public static readonly Error ImageUploadFailed = new Error(
    "User.PhotoUploadFailed", "Failed to save photo!");

}
