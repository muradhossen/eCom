namespace Application.DTOs.Search;

public class SearchDto
{
    /// <summary>
    /// A item can be category, subcategory or service.
    /// And ItemId will be categoryId subcategoryId or serviceId
    /// </summary>
    public long ItemId { get; set; }
    public string ItemName { get; set; }
    public double? Price { get; set; }
    public string Type { get; set; }
    /// <summary>
    /// It will be the parent id of the item.
    /// If the item is product then ParentId will be subcategoryId.
    /// Else if it is subcategory then it will be categoryId.
    /// Else if it is category then parentId will be null.Because 
    /// category is the root level of the hierarchy.
    /// </summary>
    public long? ParentId { get; set; }
    public string Code { get; set; } 
    public string ImageUrl { get; set; }
}
