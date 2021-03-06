using System;
using System.IO;
using Tommy;

namespace ScanFolders.Classes;

public static class SettingsFile
{
    public static string TranslationFolder = "08-Translation";
    public static string ProofreadFolder = "09-Proofread";
    public static string RawsFolder = "01-Raws";
    public static string ClRdFolder = "02-CLRD";
    public static string TsFolder = "03-TS";
    public static string QcFolder = "04-QC";
    public static string ChapterFolder = "Chapter ";


    public static void GetSettings()
    {
        if (!File.Exists("config.toml")) CreateFile();

        using (var reader = File.OpenText("config.toml"))
        {
            var table = TOML.Parse(reader);

            TranslationFolder = table["folders"]["Translation"];
            ProofreadFolder = table["folders"]["Proofread"];
            RawsFolder = table["folders"]["Raws"];
            ClRdFolder = table["folders"]["CLRD"];
            TsFolder = table["folders"]["TS"];
            QcFolder = table["folders"]["QC"];
            ChapterFolder = table["chapters"]["Prefix"];
        }
    }

    public static int UpdateSettings(string tl, string pr, string raws, string clrd, string ts, string qc,
        string prefix)
    {
        if (tl == "" || pr == "" || raws == "" || clrd == "" || ts == "" || qc == "" || prefix == "")
            return 745;
        try
        {
            var toml = new TomlTable //TODO: Find way to edit instead of remaking
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
            using (var writer = new StreamWriter("config.toml"))
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

            ErrorMessages.ToErrorMessage(1);
            return 1;
        }
    }

    public static void CreateFile()
    {
        var toml = new TomlTable
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
            using (var writer = File.CreateText("config.toml"))
            {
                toml.WriteTo(writer);
                writer.Flush();
            }
        }
        catch (Exception e)
        {
            ErrorMessages.ToErrorMessage(e is UnauthorizedAccessException ? 102 : 1);
        }
    }
}