//TODO edge case: Bonus chapters for chapters with split > 10 need to go to .50

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScanFolders.Classes;

public static class Chapters
{
    private static List<string> _bonusList = new();

    public static void CreateChapter(int bonusSel, int split, int begin, int amount, string path, string bonusCh,
        bool? tl, bool? pr)
    {
        var dir = SettingsFile.ChapterFolder;
        for (var i = begin; i < amount + begin; i++)
            if (split is 0 or 1)
            {
                Directory.CreateDirectory(path + "/" + dir + i + "/");
            }
            else
            {
                // Incredibly over-engineered, but stops folders from appending with .01 etc.
                // TODO: Find fix for above
                var o = 1;
                for (var j = 0; j < split; j++)
                {
                    var splitsLoop = 0;
                    while (splitsLoop == 0)
                        if (Directory.Exists(path + "/" + dir + i + "." + o + "/"))
                        {
                            o++;
                        }
                        else
                        {
                            Directory.CreateDirectory(path + "/" + dir + i + "." + o + "/");
                            splitsLoop++;
                        }
                }
            }

        if (bonusSel != 0) BonusChapter(bonusSel, bonusCh, path, begin, amount, dir, split);
        if (tl == true) Directory.CreateDirectory(path + "/Translations/");
        if (pr == true) Directory.CreateDirectory(path + "/Proofread/");
    }

    private static void BonusChapter(int bonusSel, string bonusCh, string path, int begin, int amount, string dir,
        int split)
    {
        if (bonusSel == 1)
        {
            _bonusList = bonusCh.Split(',').ToList();
            var bonusListInt = _bonusList.Select(int.Parse).ToList();
            var bonusMax = (from i in bonusListInt
                group i by i
                into grp
                orderby grp.Count() descending
                select grp.Count()).First();
            var bonusMaxInt = (from i in bonusListInt
                group i by i
                into grp
                orderby grp.Count() descending
                select grp.Key).First();
            if (split >= 5)
                foreach (var bonusInt in _bonusList.Select(bonus => Convert.ToInt32(bonus)))
                    if (split + bonusMax >= 10 && bonusInt == bonusMaxInt)
                    {
                        var o = split * 10 + 10; //TODO: Make this more efficient
                        var bonusLoop = 0;
                        while (bonusLoop == 0)
                            if (Directory.Exists(path + "/" + dir + bonusInt + "." + o + "/"))
                            {
                                o++;
                            }
                            else
                            {
                                Directory.CreateDirectory(path + "/" + dir + bonusInt + "." + o + "/");
                                bonusLoop++;
                            }
                    }

                    else
                    {
                        var o = split + 1; //TODO: Make this more efficient
                        var bonusLoop = 0;
                        while (bonusLoop == 0)
                            if (Directory.Exists(path + "/" + dir + bonusInt + "." + o + "/"))
                            {
                                o++;
                            }
                            else
                            {
                                Directory.CreateDirectory(path + "/" + dir + bonusInt + "." + o + "/");
                                bonusLoop++;
                            }
                    }
            else
                foreach (var bonusInt in _bonusList.Select(bonus => Convert.ToInt32(bonus)))
                    if (split + bonusMax >= 10 && bonusInt == bonusMaxInt)
                    {
                        var o = 50; //TODO: Make this more efficient
                        var bonusLoop = 0;

                        while (bonusLoop == 0)
                            if (Directory.Exists(path + "/" + dir + bonusInt + "." + o + "/"))
                            {
                                o++;
                            }
                            else
                            {
                                Directory.CreateDirectory(path + "/" + dir + bonusInt + "." + o + "/");
                                bonusLoop++;
                            }
                    }

                    else
                    {
                        var o = 5; //TODO: Make this more efficient
                        var bonusLoop = 0;

                        while (bonusLoop == 0)
                            if (Directory.Exists(path + "/" + dir + bonusInt + "." + o + "/"))
                            {
                                o++;
                            }
                            else
                            {
                                Directory.CreateDirectory(path + "/" + dir + bonusInt + "." + o + "/");
                                bonusLoop++;
                            }
                    }
        }
        else
        {
            var bonusInt = Convert.ToInt32(bonusCh);
            if (split >= 5)
            {
                if (split + bonusInt >= 10)
                    for (var i = begin; i < amount + begin; i++)
                    for (var j = 0; j < bonusInt; j++)
                    {
                        var o = split * 10 + 10; //TODO: Make this more efficient
                        var bonusLoop = 0;
                        while (bonusLoop == 0)
                            if (Directory.Exists(path + "/" + dir + i + "." + o + "/"))
                            {
                                o++;
                            }
                            else
                            {
                                Directory.CreateDirectory(path + "/" + dir + i + "." + o + "/");
                                bonusLoop++;
                            }
                    }
                else
                    for (var i = begin; i < amount + begin; i++)
                    for (var j = 0; j < bonusInt; j++)
                    {
                        var o = split + 1; //TODO: Make this more efficient
                        var bonusLoop = 0;
                        while (bonusLoop == 0)
                            if (Directory.Exists(path + "/" + dir + i + "." + o + "/"))
                            {
                                o++;
                            }
                            else
                            {
                                Directory.CreateDirectory(path + "/" + dir + i + "." + o + "/");
                                bonusLoop++;
                            }
                    }
            }
            else
            {
                if (split + bonusInt >= 10)
                    for (var i = begin; i < amount + begin; i++)
                    for (var j = 0; j < bonusInt; j++)
                    {
                        var o = 50; //TODO: Make this more efficient
                        var bonusLoop = 0;
                        while (bonusLoop == 0)
                            if (Directory.Exists(path + "/" + dir + i + "." + o + "/"))
                            {
                                o++;
                            }
                            else
                            {
                                Directory.CreateDirectory(path + "/" + dir + i + "." + o + "/");
                                bonusLoop++;
                            }
                    }
                else
                    for (var i = begin; i < amount + begin; i++)
                    for (var j = 0; j < bonusInt; j++)
                    {
                        var o = 5; //TODO: Make this more efficient
                        var bonusLoop = 0;
                        while (bonusLoop == 0)
                            if (Directory.Exists(path + "/" + dir + i + "." + o + "/"))
                            {
                                o++;
                            }
                            else
                            {
                                Directory.CreateDirectory(path + "/" + dir + i + "." + o + "/");
                                bonusLoop++;
                            }
                    }
            }
        }
    }
}