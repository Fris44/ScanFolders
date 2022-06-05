using System.ComponentModel;
using ScanFolders.Classes;

namespace ScanFolders.Viewmodels;

public class MainWindowViewModel  : INotifyPropertyChanged
{
    string translation = SettingsFile.TranslationFolder;
    string proofread = SettingsFile.ProofreadFolder;
    string raws = SettingsFile.RawsFolder;
    string clrd = SettingsFile.ClRdFolder;
    string ts = SettingsFile.TsFolder;
    string qc = SettingsFile.QcFolder;
    string pre = SettingsFile.ChapterFolder;


    public event PropertyChangedEventHandler PropertyChanged;

    public string Translation
    {
        get { return translation; }
        set
        {
            if (translation != value)
            {
                translation = value;
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(nameof(Translation)));
                }
            }
        }
    }
    
    public string Proofread
    {
        get { return proofread; }
        set
        {
            if (proofread != value)
            {
                proofread = value;
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(nameof(Proofread)));
                }
            }
        }
    }
    
    public string Raws
    {
        get { return raws; }
        set
        {
            if (raws != value)
            {
                raws = value;
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(nameof(Raws)));
                }
            }
        }
    }
    
    public string ClRd
    {
        get { return clrd; }
        set
        {
            if (clrd != value)
            {
                clrd = value;
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(nameof(ClRd)));
                }
            }
        }
    }
    
    public string Ts
    {
        get { return ts; }
        set
        {
            if (ts != value)
            {
                ts = value;
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(nameof(Ts)));
                }
            }
        }
    }
    
    public string Qc
    {
        get { return qc; }
        set
        {
            if (qc != value)
            {
                qc = value;
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(nameof(Qc)));
                }
            }
        }
    }
    
    public string Prefix
    {
        get { return pre; }
        set
        {
            if (pre != value)
            {
                pre = value;
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(nameof(Prefix)));
                }
            }
        }
    }
}