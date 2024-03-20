using Application.Common;
using System.Data;
using System.Reflection;

namespace Application.Extentions;

public static class ICollectionExtention
{
    public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
    {
        if (collection is not null || collection.Count == 0)
        {
            return true;
        }
        return false;
    }

    public static ICollection<T> SetSlNumber<T>(this ICollection<T> collection, int currentPage, int pageSize)
    {
        if (collection is not null || collection.Count == 0)
        {

            int startingIndex = (currentPage - 1) * pageSize;
            int slNumber = startingIndex + 1;

            foreach (var item in collection)
            {
                SetSlProperty(item, slNumber++);
            }


        }
        return collection;
    }
    private static void SetSlProperty<T>(T item, int slNumber)
    {
        PropertyInfo slProperty = typeof(T).GetProperty("Sl");
        if (slProperty != null && slProperty.CanWrite)
        {
            slProperty.SetValue(item, slNumber);
        }
    }
}
