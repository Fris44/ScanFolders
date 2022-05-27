using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScanFolders.Classes;

public class Chapters
{
    private static List<string> bonusList = new List<string>();

    public static void CreateChapter(int bonusSel, int split, int begin, int amount, string path, string bonusCh)
    {
        for (int i = begin; i < amount + begin; i++)
        {
            if (split is 0 or 1)
            {
                Directory.CreateDirectory(path + "/ch " + (i) + "/");
            }
            else
            {
                int o = 1;
                for (int j = 0; j < split; j++)
                {
                    int splitsLoop = 0;
                    while (splitsLoop == 0)
                    {
                        if (Directory.Exists(path + "/ch " + (i) + "." + o + "/"))
                        {
                            o++;
                        }
                        else
                        {
                            Directory.CreateDirectory(path + "/ch " + (i) + "." + o + "/");
                            splitsLoop++;
                        }
                    }
                }
            }
        }

        if (bonusSel != 0)
        {
            BonusChapter(bonusSel, bonusCh, path, begin, amount);
        }
    }

    static void BonusChapter(int bonusSel, string bonusCh, string path, int begin, int amount) //TODO: Add support for split + bonus chapters that exceed *.9
    {
        if (bonusSel == 1)
        {
            bonusList = bonusCh.Split(',').ToList();
            foreach (var bonus in bonusList)
            {
                int bonusInt = Convert.ToInt32(bonus);
                int o = 5; //TODO: Make this more efficient
                int splitsLoop = 0;
                
                while (splitsLoop == 0)
                {
                    if (Directory.Exists(path + "/ch " + bonusInt + "." + o + "/"))
                    {
                        o++;
                    }
                    else
                    { 
                        Directory.CreateDirectory(path + "/ch " + bonusInt + "." + o + "/");
                        splitsLoop++;
                    }
                }
            }
        }
        else
        {
            int bonusInt = Convert.ToInt32(bonusCh);
            for (int i = begin; i < amount + begin; i++)
            {
                for (int j = 0; j < bonusInt; j++)
                {
                    int o = 5; //TODO: Make this more efficient
                    int splitsLoop = 0;
                    while (splitsLoop == 0)
                    {
                        if (Directory.Exists(path + "/ch " + (i) + "." + o + "/"))
                        {
                            o++;
                        }
                        else
                        {
                            Directory.CreateDirectory(path + "/ch " + (i) + "." + o + "/");
                            splitsLoop++;
                        }
                    }
                }
            }
        }
    }
}
