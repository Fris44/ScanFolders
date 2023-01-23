// TODO: Make cleaner dialog

using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using ScanFolders.Viewmodels;
using ScanFolders.Classes;

namespace ScanFolders.Views;

public partial class ErrorDialog : Window
{
    private readonly ErrorDialogViewModel vm;

    public ErrorDialog()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        vm = new ErrorDialogViewModel();
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