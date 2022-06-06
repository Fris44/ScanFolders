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
            if (error != value)
            {
                error = value;
                var handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(nameof(Error)));
            }
        }
    }


    public event PropertyChangedEventHandler PropertyChanged;
}