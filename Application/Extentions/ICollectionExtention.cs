namespace Application.Extentions;

public static class ICollectionExtention
{
    public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
    {
        if (collection is null || collection.Count == 0)
        {
            return true;
        }
        return false;
    }
}
