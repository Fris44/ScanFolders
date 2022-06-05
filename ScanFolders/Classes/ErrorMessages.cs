using ScanFolders.Views;

namespace ScanFolders.Classes;

public class ErrorMessages
{
    public static string Message = null!;
    public static int Error = 0;
    public static void ToErrorMessage(int error)
    {
        Message = error switch
        {
            101 => "Insufficient permissions. Try running the program as administrator or choose a different directory",
            44 => "One of the text fields contains a character that is not a number or comma",
            38 => "Not yet implemented",
            745 => "One of the inputs is empty",
            102 => "Insufficient permissions to create configuration file. Try running the program as administrator or choose a different directory",
            0 => "Nothing went wrong. If this pops up it means some code needs fixing!",
            8 => "Saved successfully",
            _ => "Something went wrong"
        };
    }
}