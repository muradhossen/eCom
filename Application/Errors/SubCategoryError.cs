using Application.Common;

namespace Application.Errors;

public static class SubCategoryError
{
    public static readonly Error NotFound = new Error(
        "SubCategories.SubCategoryNotFound", "The requested subcategory was not found!");

    public static Error UpdateFailed(string code) => new Error(
        "SubCategories.UpdateFailed", $"Failed to update subcategory with code {code}");

    public static readonly Error CreateFailed = new Error(
        "SubCategories.CreateFailed", "Failed to create subcategory");

    public static readonly Error DeleteFailed = new Error(
        "SubCategories.DeleteFailed", "Failed to delete subcategory");

    public static readonly Error ImageUploadFailed = new Error(
        "SubCategory.PhotoUploadFailed", "Failed to save photo!");
}
