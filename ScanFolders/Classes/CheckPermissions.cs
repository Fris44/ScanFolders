using System;
using System.IO;

namespace ScanFolders.Classes;

public static class CheckPermissions
{
    public static bool IsDirectoryWritable(string dirPath, bool throwIfFails = false)
    {
        try
        {
            using var fs = File.Create(
                Path.Combine(
                    dirPath,
                    Path.GetRandomFileName()
                ),
                1,
                FileOptions.DeleteOnClose);

            return true;
        }
        catch (UnauthorizedAccessException)
        {
            ErrorMessages.ToErrorMessage(101);

            if (throwIfFails)
                throw;
            return false;
        }
    }
}