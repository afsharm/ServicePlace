namespace ServicePlace.Model.Constants;

public class ErrorMessageConstants
{
    public const string DuplicateServiceName = "Duplicate service `name`.";
    public const string IsNullOrWhiteSpace = "Argument is null or white space";
    public const string ShouldNoStartOrEnd = "Argument should not start or end with space(s)";
    public const string ShouldNotBeSmaller = "Argument should not be smaller than 3 or bigger than 100 characters";
}