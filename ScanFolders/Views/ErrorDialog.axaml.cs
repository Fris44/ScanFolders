using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ScanFolders.Viewmodels;

namespace ScanFolders.Views;

public partial class ErrorDialog : Window
{
    private ErrorDialogViewModel vm;
    public ErrorDialog()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        vm = new ErrorDialogViewModel();
        this.DataContext = vm;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    private void CloseBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}