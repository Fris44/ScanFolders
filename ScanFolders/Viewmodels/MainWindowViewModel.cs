using System.ComponentModel;
using ScanFolders.Classes;

namespace ScanFolders.Viewmodels;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private string clrd = SettingsFile.ClRdFolder;
    private string pre = SettingsFile.ChapterFolder;
    private string proofread = SettingsFile.ProofreadFolder;
    private string qc = SettingsFile.QcFolder;
    private string raws = SettingsFile.RawsFolder;
    private string translation = SettingsFile.TranslationFolder;
    private string ts = SettingsFile.TsFolder;

    public string Translation
    {
        get => translation;
        set
        {
            if (translation == value) return;
            translation = value;
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(nameof(Translation)));
        }
    }

    public string Proofread
    {
        get => proofread;
        set
        {
            if (proofread == value) return;
            proofread = value;
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(nameof(Proofread)));
        }
    }

    public string Raws
    {
        get => raws;
        set
        {
            if (raws == value) return;
            raws = value;
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(nameof(Raws)));
        }
    }

    public string ClRd
    {
        get => clrd;
        set
        {
            if (clrd == value) return;
            clrd = value;
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(nameof(ClRd)));
        }
    }

    public string Ts
    {
        get => ts;
        set
        {
            if (ts == value) return;
            ts = value;
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(nameof(Ts)));
        }
    }

    public string Qc
    {
        get => qc;
        set
        {
            if (qc == value) return;
            qc = value;
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(nameof(Qc)));
        }
    }

    public string Prefix
    {
        get => pre;
        set
        {
            if (pre == value) return;
            pre = value;
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(nameof(Prefix)));
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;
}