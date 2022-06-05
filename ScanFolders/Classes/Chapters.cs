﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScanFolders.Classes;

public static class Chapters
{
    private static List<string> bonusList = new List<string>();

    public static void CreateChapter(int bonusSel, int split, int begin, int amount, string path, string bonusCh, bool? tl, bool? pr)
    {
        string dir = SettingsFile.ChapterFolder;
        for (int i = begin; i < amount + begin; i++)
        {
            if (split is 0 or 1)
            {
                Directory.CreateDirectory(path + "/" + dir + (i) + "/");
            }
            else
            {
                int o = 1;
                for (int j = 0; j < split; j++)
                {
                    int splitsLoop = 0;
                    while (splitsLoop == 0)
                    {
                        if (Directory.Exists(path + "/" + dir + (i) + "." + o + "/"))
                        {
                            o++;
                        }
                        else
                        {
                            Directory.CreateDirectory(path + "/" + dir + (i) + "." + o + "/");
                            splitsLoop++;
                        }
                    }
                }
            }
        }

        if (bonusSel != 0)
        {
            BonusChapter(bonusSel, bonusCh, path, begin, amount, dir, split);
        }

        if (tl == true)
        {
            Directory.CreateDirectory(path + "/00-Translations/");
        }

        if (pr == true)
        {
            Directory.CreateDirectory(path + "/01-Proofread/");
        }
    }

    static void BonusChapter(int bonusSel, string bonusCh, string path, int begin, int amount, string dir, int split)
    {
        if (bonusSel == 1)
        {
            bonusList = bonusCh.Split(',').ToList();
            List<int> bonusListInt = bonusList.Select(int.Parse).ToList();
            int bonusMax = bonusListInt.Max();
            if (split >= 5)
            {
                if (split + bonusMax >= 10)
                {
                    foreach (var bonus in bonusList)
                    {
                        int bonusInt = Convert.ToInt32(bonus);
                        int o = (split * 10) + 10; //TODO: Make this more efficient
                        int bonusLoop = 0;

                        while (bonusLoop == 0)
                        {
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
                }
                else
                {
                    foreach (var bonus in bonusList)
                    {
                        int bonusInt = Convert.ToInt32(bonus);
                        int o = split + 1; //TODO: Make this more efficient
                        int bonusLoop = 0;

                        while (bonusLoop == 0)
                        {
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
                }
            }
            else
            {
                if (split + bonusMax >= 10)
                {
                    foreach (var bonus in bonusList)
                    {
                        int bonusInt = Convert.ToInt32(bonus);
                        int o = 50; //TODO: Make this more efficient
                        int bonusLoop = 0;

                        while (bonusLoop == 0)
                        {
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
                }
                else
                {
                    foreach (var bonus in bonusList)
                    {
                        int bonusInt = Convert.ToInt32(bonus);
                        int o = 5; //TODO: Make this more efficient
                        int bonusLoop = 0;

                        while (bonusLoop == 0)
                        {
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
                }
            }
        }
        else
        {
            int bonusInt = Convert.ToInt32(bonusCh);
            if (split >= 5)
            {
                if (split + bonusInt >= 10)
                {
                    for (int i = begin; i < amount + begin; i++)
                    {
                        for (int j = 0; j < bonusInt; j++)
                        {
                            int o = (split * 10) + 10; //TODO: Make this more efficient
                            int bonusLoop = 0;
                            while (bonusLoop == 0)
                            {
                                if (Directory.Exists(path + "/ch " + (i) + "." + o + "/"))
                                {
                                    o++;
                                }
                                else
                                {
                                    Directory.CreateDirectory(path + "/ch " + (i) + "." + o + "/");
                                    bonusLoop++;
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = begin; i < amount + begin; i++)
                    {
                        for (int j = 0; j < bonusInt; j++)
                        {
                            int o = split + 1; //TODO: Make this more efficient
                            int bonusLoop = 0;
                            while (bonusLoop == 0)
                            {
                                if (Directory.Exists(path + "/ch " + (i) + "." + o + "/"))
                                {
                                    o++;
                                }
                                else
                                {
                                    Directory.CreateDirectory(path + "/ch " + (i) + "." + o + "/");
                                    bonusLoop++;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (split + bonusInt >= 10)
                {
                    for (int i = begin; i < amount + begin; i++)
                    {
                        for (int j = 0; j < bonusInt; j++)
                        {
                            int o = 50; //TODO: Make this more efficient
                            int bonusLoop = 0;
                            while (bonusLoop == 0)
                            {
                                if (Directory.Exists(path + "/ch " + (i) + "." + o + "/"))
                                {
                                    o++;
                                }
                                else
                                {
                                    Directory.CreateDirectory(path + "/ch " + (i) + "." + o + "/");
                                    bonusLoop++;
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = begin; i < amount + begin; i++)
                    {
                        for (int j = 0; j < bonusInt; j++)
                        {
                            int o = 5; //TODO: Make this more efficient
                            int bonusLoop = 0;
                            while (bonusLoop == 0)
                            {
                                if (Directory.Exists(path + "/ch " + (i) + "." + o + "/"))
                                {
                                    o++;
                                }
                                else
                                {
                                    Directory.CreateDirectory(path + "/ch " + (i) + "." + o + "/");
                                    bonusLoop++;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
