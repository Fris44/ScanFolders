using ScanFolders.Views;

namespace ScanFolders.Classes;

public class ErrorMessages
{
    public static string Message = null!;
    public static void ToErrorMessage(int error)
    {
        Message = error switch
        {
            101 => "Insufficient permissions. Try running the program as administrator or choose a different directory",
            44 => "One of the text fields contains a character that is not a number or comma",
            38 => "Not yet implemented",
            _ => "Something went wrong"
        };
    }
}