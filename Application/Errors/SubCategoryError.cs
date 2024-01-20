using Application.Common;

namespace Application.Errors;

public static class SubCategoryError
{
    public static readonly Error NotFound = new Error(
   "Categories.CategoryNotFound", "The requested subcategory was not found!");

    public static Error UpdateFailed(string code) => new Error(
        "Categories.UpdateFailed", $"Failed to update subcategory with code {code}");

    public static readonly Error CreateFailed = new Error(
        "Categories.CreateFailed", "Failed to create category");
}
