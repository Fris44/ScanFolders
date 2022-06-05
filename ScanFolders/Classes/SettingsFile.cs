using System;
using System.IO;
using System.Net.Mime;
using Avalonia;
using Tommy;

namespace ScanFolders.Classes;

public class SettingsFile
{
    public static string TranslationFolder = "";
    public static string ProofreadFolder = "";
    public static string RawsFolder = "";
    public static string ClRdFolder = "";
    public static string TsFolder = "";
    public static string QcFolder = "";
    public static string ChapterFolder = "";


    public static void GetSettings()
    {
        if (!File.Exists("config.toml"))
        {
            CreateFile();
        }

        using (StreamReader reader = File.OpenText("config.toml"))
        {
            TomlTable table = TOML.Parse(reader);
            
            TranslationFolder = table["folders"]["Translation"];
            ProofreadFolder = table["folders"]["Proofread"];
            RawsFolder = table["folders"]["Raws"];
            ClRdFolder = table["folders"]["CLRD"];
            TsFolder = table["folders"]["TS"];
            QcFolder = table["folders"]["QC"];
            ChapterFolder = table["chapters"]["Prefix"];
        }
    }

    public static int UpdateSettings(string tl, string pr, string raws, string clrd, string ts, string qc, string prefix)
    {
        if (tl == "" || pr == "" || raws == "" || clrd == "" || ts == "" || qc == "" || prefix == "")
        {
            return 745;
        }
        else
        {
            try
            {
                TomlTable toml = new TomlTable //TODO: Find way to edit instead of remaking
                {
                    ["title"] = "ScanFolder Settings",

                    ["folders"] =
                    {
                        ["Translation"] = tl,
                        ["Proofread"] = pr,
                        ["Raws"] = raws,
                        ["CLRD"] = clrd,
                        ["TS"] = ts,
                        ["QC"] = qc
                    },
                    
                    ["chapters"] =
                    {
                        ["Prefix"] = prefix
                    }
                };
                using (StreamWriter writer = new StreamWriter("config.toml"))
                {
                    toml.WriteTo(writer);
                    writer.Flush();
                }
                ErrorMessages.ToErrorMessage(8);
                return 8;
            }
            catch (Exception e)
            {
                if (e is UnauthorizedAccessException)
                {
                    ErrorMessages.ToErrorMessage(101);
                    return 101;
                }
                else
                {
                    ErrorMessages.ToErrorMessage(1);
                    return 1;
                }
            }
        }
    }

    public static void CreateFile()
    {
        TomlTable toml = new TomlTable
        {
            ["title"] = "ScanFolder Settings",

            ["folders"] =
            {
                ["Translation"] = "08-Translation",
                ["Proofread"] = "09-Proofread",
                ["Raws"] = "01-Raws",
                ["CLRD"] = "02-CLRD",
                ["TS"] = "03-TS",
                ["QC"] = "04-QC"
            },
            
            ["chapters"] =
            {
                ["Prefix"] = "Chapter "
            }
        };

        try
        {
            using (StreamWriter writer = File.CreateText("config.toml"))
            {
                toml.WriteTo(writer);
                writer.Flush();
            }
        }
        catch (Exception e)
        {
            if (e is UnauthorizedAccessException)
            {
                ErrorMessages.ToErrorMessage(102);
            }
            else
            {
                ErrorMessages.ToErrorMessage(1);
            }
        }
        
    }
}