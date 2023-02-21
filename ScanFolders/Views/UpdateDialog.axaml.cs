using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace ScanFolders.Views;

public partial class UpdateDialog : Window
{
    public UpdateDialog()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void CloseBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    private void DownloadBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo
            { FileName = "https://github.com/TheFrisianGamer/ScanFolders/releases/latest", UseShellExecute = true });
    }
}