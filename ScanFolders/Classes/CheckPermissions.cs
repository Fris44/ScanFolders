﻿using System;
using System.IO;

namespace ScanFolders.Classes;

public class CheckPermissions
{
    public static bool IsDirectoryWritable(string dirPath, bool throwIfFails = false)
    {
        try
        {
            using (FileStream fs = File.Create(
                       Path.Combine(
                           dirPath, 
                           Path.GetRandomFileName()
                       ), 
                       1,
                       FileOptions.DeleteOnClose)
                  )
            { }
            return true;
        }
        catch (UnauthorizedAccessException)
        {
            ErrorMessages.ToErrorMessage(101);
            
            if (throwIfFails)
                throw;
            else
                return false;
        }
    }
}