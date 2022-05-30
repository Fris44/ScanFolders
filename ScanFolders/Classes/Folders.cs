using System.IO;
using HarfBuzzSharp;

namespace ScanFolders.Classes;

public static class Folders
{
    public static void CreateFolders(string path, bool? tl, bool? pr, bool? raw, bool? clrd, bool? ts, bool? qc)
    {
        string[] dirs = Directory.GetDirectories(path);
        foreach (var dir in dirs) //TODO: Find if there's a better way to do this instead of multiple if-statements
        {
            if (dir == path + "/00-Translations" || dir == path + "/01-Proofread" || dir == path + "/Translations" ||
                dir == path + "/Proofread" || dir == path + "/translations" ||  dir == path + "/proofread" ||
                dir == path + "/Scripts" || dir == path + "/scripts" || dir == path + "/TL" || dir == path + "/tl" ||
                dir == path + "/Tl" || dir == path + "/PR" || dir == path + "/pr" || dir == path + "/Pr") return; //TODO: Set-up a regex or something to get rid of this abomination
            if (tl == true)
            {
                Directory.CreateDirectory(dir + "/08-TL/");
            }

            if (pr == true)
            {
                Directory.CreateDirectory(dir + "/09-PR/");
            }

            if (raw == true)
            {
                Directory.CreateDirectory(dir + "/00-RAWS/");

                string[] files = Directory.GetFiles(dir);
                foreach (var file in files)
                {
                    string fileName = Path.GetFileName(file);
                    string destFile = Path.Combine(dir + "/00-RAWS/", fileName);
                    File.Move(file, destFile);
                }
            }

            if (clrd == true)
            {
                Directory.CreateDirectory(dir + "/01-CLRD/");
            }

            if (ts == true)
            {
                Directory.CreateDirectory(dir + "/02-TS/");
            }

            if (qc == true)
            {
                Directory.CreateDirectory(dir + "/03-QC");
            }
        }
    }
}