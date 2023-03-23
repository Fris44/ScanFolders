using System;
using System.Collections.Generic;
using System.IO;
using FrisExtras.Misc;
using Tommy;

namespace ScanFolders.Classes;

public static class SettingsFile
{
    public static void GetSettings()
    {
        if (!File.Exists("config.toml")) CreateFile();

        using var reader = File.OpenText("config.toml");
        var table = TOML.Parse(reader);

        TranslationFolder = table["folders"]["Translation"];
        ProofreadFolder = table["folders"]["Proofread"];
        RawsFolder = table["folders"]["Raws"];
        ClRdFolder = table["folders"]["CLRD"];
        TsFolder = table["folders"]["TS"];
        QcFolder = table["folders"]["QC"];
        ChapterFolder = table["chapters"]["Prefix"];
        if (!table["update"]["Startup"].HasValue)
            UpdateSettings(TranslationFolder, ProofreadFolder, RawsFolder, ClRdFolder, TsFolder, QcFolder,
                ChapterFolder, StartupCheck);
        else
            StartupCheck = table["update"]["Startup"];
    }

    public static int UpdateSettings(string tl, string pr, string raws, string clrd, string ts, string qc,
        string prefix, bool? startup)
    {
        var strList = new List<string>
        {
            tl,
            pr,
            raws,
            clrd,
            ts,
            qc,
            prefix
        };

        if (Misc.WhitespaceCheck(strList))
            return 745;

        if (Misc.IllegalCharCheck(strList))
            return 746;

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
                },

                ["update"] =
                {
                    ["Startup"] = startup
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

    private static void CreateFile()
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
            },

            ["update"] =
            {
                ["Startup"] = true
            }
        };

        try
        {
            using var writer = File.CreateText("config.toml");
            toml.WriteTo(writer);
            writer.Flush();
        }
        catch (Exception e)
        {
            ErrorMessages.ToErrorMessage(e is UnauthorizedAccessException ? 102 : 1);
        }
    }
    /*
     * It is safe to suppress a warning from this rule if you are developing an application
     * and therefore have full control over access to the type that contains the static field.
     * https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca2211#when-to-suppress-warnings
     */
    // This might still be incorrect practice but I honestly can't be bothered to change it
#pragma warning disable CA2211
    public static string TranslationFolder = "08-Translation";
    public static string ProofreadFolder = "09-Proofread";
    public static string RawsFolder = "01-Raws";
    public static string ClRdFolder = "02-CLRD";
    public static string TsFolder = "03-TS";
    public static string QcFolder = "04-QC";
    public static string ChapterFolder = "Chapter ";
    public static bool StartupCheck = true;
#pragma warning restore CA2211
}