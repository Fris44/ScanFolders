// TODO: Make cleaner dialog

using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ScanFolders.Viewmodels;

namespace ScanFolders.Views;

public partial class ErrorDialog : Window
{
    public ErrorDialog()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        var vm = new ErrorDialogViewModel();
        DataContext = vm;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void CloseBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}