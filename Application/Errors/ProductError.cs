using Application.Common;

namespace Application.Errors;

public static class ProductError
{
    public static readonly Error NotFound = new Error(
       "Categories.CategoryNotFound", "The requested product was not found!");

    public static Error UpdateFailed(string code) => new Error(
        "Categories.UpdateFailed", $"Failed to update product with id {code}");

    public static readonly Error CreateFailed = new Error(
        "Categories.CreateFailed", "Failed to create product");
}
