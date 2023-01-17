namespace ScanFolders.Classes;

public static class ErrorMessages
{
/*
 * It is safe to suppress a warning from this rule if you are developing an application
 * and therefore have full control over access to the type that contains the static field.
 * https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca2211#when-to-suppress-warnings
*/
#pragma warning disable CA2211
    public static string Message = null!;
#pragma warning restore CA2211

    public static void ToErrorMessage(int error)
    {
        Message = error switch
        {
            101 => "Insufficient permissions. Try running the program as administrator or choose a different directory",
            44 => "One of the text fields contains a character that is not a number or comma",
            38 => "Not yet implemented",
            745 => "One of the inputs is empty",
            102 =>
                "Insufficient permissions to create configuration file. Try running the program as administrator or choose a different directory",
            0 => "Nothing went wrong. If this pops up it means some code needs fixing!",
            8 => "Saved successfully",
            746 => "One of the fields contains an illegal character ( < > : \" / \\ | ? * )",
            _ => "Something went wrong"
        };
    }
}