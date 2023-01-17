using System;
using System.IO;
using static ScanFolders.Classes.SettingsFile;

namespace ScanFolders.Classes;

public static class Folders
{
    public static void CreateFolders(string path, bool? tl, bool? pr, bool? raw, bool? clrd, bool? ts, bool? qc)
    {
        var dirs = Directory.GetDirectories(path);
        foreach (var dir in dirs) //TODO: Find if there's a better way to do this instead of multiple if-statements
        {
            if (dir.Equals(path + "/00-translations", StringComparison.OrdinalIgnoreCase) ||
                dir.Equals(path + "/01-proofread", StringComparison.OrdinalIgnoreCase) ||
                dir.Equals(path + "/translations", StringComparison.OrdinalIgnoreCase) ||
                dir.Equals(path + "/proofread", StringComparison.OrdinalIgnoreCase) ||
                dir.Equals(path + "/scripts", StringComparison.OrdinalIgnoreCase) ||
                dir.Equals(path + "/script", StringComparison.OrdinalIgnoreCase) ||
                dir.Equals(path + "/tl", StringComparison.OrdinalIgnoreCase) ||
                dir.Equals(path + "/pr", StringComparison.OrdinalIgnoreCase)) return; //Oh well
            if (tl == true) Directory.CreateDirectory(dir + "/" + TranslationFolder + "/");

            if (pr == true) Directory.CreateDirectory(dir + "/" + ProofreadFolder + "/");

            if (raw == true)
            {
                Directory.CreateDirectory(dir + "/" + RawsFolder + "/");

                var files = Directory.GetFiles(dir);
                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);
                    var destFile = Path.Combine(dir + "/" + RawsFolder + "/", fileName);
                    File.Move(file, destFile);
                }
            }

            if (clrd == true) Directory.CreateDirectory(dir + "/" + ClRdFolder + "/");

            if (ts == true) Directory.CreateDirectory(dir + "/" + TsFolder + "/");

            if (qc == true) Directory.CreateDirectory(dir + "/" + QcFolder + "/");
        }
    }
}