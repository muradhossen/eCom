using Application.Common;

namespace Application.Errors;

public static class CategoryError
{

    public static readonly Error NotFound = new Error(
        "Categories.CategoryNotFound", "The requested category was not found!");

    public static Error UpdateFailed(string code) => new Error(
        "Categories.UpdateFailed", $"Failed to update category with id {code}");

    public static readonly Error CreateFailed = new Error(
        "Categories.CreateFailed", "Failed to create category");

    public static readonly Error DeleteFailed = new Error(
    "Categories.DeleteFailed", "Failed to delete category");
}
