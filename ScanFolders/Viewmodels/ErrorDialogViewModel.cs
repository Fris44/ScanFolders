using System.ComponentModel;
using ScanFolders.Classes;

namespace ScanFolders.Viewmodels;

public class ErrorDialogViewModel : INotifyPropertyChanged
{
    private string error = ErrorMessages.Message;

    public string Error
    {
        get => error;
        set
        {
            if (error == value) return;
            error = value;
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(nameof(Error)));
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;
}