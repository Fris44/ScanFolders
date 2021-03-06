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
            if (dir == path + "/00-Translations" || dir == path + "/01-Proofread" || dir == path + "/Translations" ||
                dir == path + "/Proofread" || dir == path + "/translations" || dir == path + "/proofread" ||
                dir == path + "/Scripts" || dir == path + "/scripts" || dir == path + "/TL" || dir == path + "/tl" ||
                dir == path + "/Tl" || dir == path + "/PR" || dir == path + "/pr" ||
                dir == path + "/Pr") return; //TODO: Set-up a regex or something to get rid of this abomination
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