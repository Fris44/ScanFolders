using System.ComponentModel;
using ScanFolders.Classes;

namespace ScanFolders.Viewmodels;

public class ErrorDialogViewModel : INotifyPropertyChanged
{
    string error = ErrorMessages.Message;


    public event PropertyChangedEventHandler PropertyChanged;

    public string Error
    {
        get { return error; }
        set
        {
            if (error != value)
            {
                error = value;
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(nameof(Error)));
                }
            }
        }
    }
}