namespace Domain.Common;

public class ValidationMessage
{
    public const string MaxLengthMessage = $"The field cannot exceed 1000 characters.";
    public const string MinLengthMessage = $"The field must be at least 1 characters long.";
    public const string RequiredMessage = $"The field is required.";
 
}
