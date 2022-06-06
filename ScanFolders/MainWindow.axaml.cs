//TODO: Comment stuff you

using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ScanFolders.Classes;
using ScanFolders.Viewmodels;
using ScanFolders.Views;

namespace ScanFolders;

public partial class MainWindow : Window
{
    private static string path = null!;
    private string amountString = null;
    private string beginString = null;
    private int bonusSel;
    private string bonusString = null;
    private int selection;
    private string splitString = null;

    private readonly MainWindowViewModel vm;


    public MainWindow()
    {
        InitializeComponent();

        vm = new MainWindowViewModel();
        DataContext = vm;
        var error = ErrorMessages.Error;
        if (error != 0) OnError();
    }

    private void ChBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        selection = 1;
        Menu.IsVisible = false;
        Sel2Lbl.IsVisible = false;
        Directory.IsVisible = true;
    }

    private void ScanBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        selection = 2;
        Menu.IsVisible = false;
        Sel2Lbl.IsVisible = true;
        Directory.IsVisible = true;
    }

    private void ExitBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }

    private async void DirBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        var dlg = new OpenFolderDialog();
        var result = await dlg.ShowAsync(this);
        if (result == null) return;
        path = result;
        DirText.Text = path;
        if (CheckPermissions.IsDirectoryWritable(path))
        {
            NxtDir.IsEnabled = true;
        }
        else
        {
            NxtDir.IsEnabled = false;
            ErrorMessages.ToErrorMessage(101);
            OnError();
        }
    }

    private void NxtDir_OnClick(object? sender, RoutedEventArgs e)
    {
        Directory.IsVisible = false;
        switch (selection)
        {
            case 1:
                ChaptersMenu.IsVisible = true;
                break;
            case 2:
                Folder.IsVisible = true;
                break;
        }
    }

    private void BckDir_OnClick(object? sender, RoutedEventArgs e)
    {
        Directory.IsVisible = false;
        Menu.IsVisible = true;
    }

    private void NxtCh_OnClick(object? sender, RoutedEventArgs e)
    {
        var charTestS = AmountTxt.Text + StartChTxt.Text + SplitTxt.Text + BonusTxt.Text; //Combine content of textboxes
        var charTest = charTestS.ToCharArray();
        char[] bonusTest = null;
        if (BonusTxt.Text != null) bonusTest = BonusTxt.Text.ToCharArray();
        //AmountTxt.Text = charTest;
        if (charTest.Any(char.IsLetter) ||
            charTest.Any(char.IsSymbol) || //Check if any of the textboxes have letters or symbols
            (charTest.Any(char.IsPunctuation) && bonusTest.Any(x => char.IsPunctuation(x) && x != ',')))
        {
            ErrorMessages.ToErrorMessage(44); //Set error code to 44 in ErrorMessages.cs
            OnError(); //Show Error dialog
        }
        else
        {
            if (AmountTxt.Text == "" || StartChTxt.Text == "" || SplitTxt.Text == "" ||
                (bonusSel != 0 && BonusTxt.Text == "") || PrefixTxt.Text == "")
            {
                ErrorMessages.ToErrorMessage(745);
                OnError();
            }
            else
            {
                var splitInt = Convert.ToInt32(SplitTxt.Text);
                var beginInt = Convert.ToInt32(StartChTxt.Text);
                var amountInt = Convert.ToInt32(AmountTxt.Text);

                ChaptersMenu.IsVisible = false;
                LoadBar.IsVisible = true;
                Chapters.CreateChapter(bonusSel, splitInt, beginInt, amountInt, path, BonusTxt.Text, TlChBox.IsChecked,
                    PrChBox.IsChecked);
                LoadBar.IsVisible = false;
                Done.IsVisible = true;
            }
        }
    }

    private void Bck_OnClick(object? sender, RoutedEventArgs e)
    {
        ChaptersMenu.IsVisible = false;
        Folder.IsVisible = false;
        Directory.IsVisible = true;
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
    }

    public void OnError()
    {
        var ed = new ErrorDialog();
        ed.ShowDialog(this);
    }

    private void BonusButtonChecked(object? sender, RoutedEventArgs e)
    {
        var rb = sender as RadioButton;

        if (rb != null)
        {
            var bonusSelection = rb.Tag.ToString();
            switch (bonusSelection)
            {
                case "BonusNone":
                    BonusTxt.IsVisible = false;
                    bonusSel = 0;
                    break;
                case "BonusSome":
                    BonusTxt.Watermark = "Enter chapters that have bonus chapters (ex. \"2,5,5,6\")";
                    BonusTxt.IsVisible = true;
                    bonusSel = 1;
                    break;
                case "BonusAll":
                    BonusTxt.Watermark = "Enter amount of bonus chapters per chapter (ex. \"1\")";
                    BonusTxt.IsVisible = true;
                    bonusSel = 2;
                    break;
            }
        }
    }

    private void MenuDir_OnClick(object? sender, RoutedEventArgs e)
    {
        Done.IsVisible = false;
        Menu.IsVisible = true;
    }

    private void NxtFolder_OnClick(object? sender, RoutedEventArgs e)
    {
        Folder.IsVisible = false;
        LoadBar.IsVisible = true;
        Folders.CreateFolders(path, TlBox.IsChecked, PrBox.IsChecked, RawsBox.IsChecked, ClrdBox.IsChecked,
            TsBox.IsChecked, QcBox.IsChecked);
        LoadBar.IsVisible = false;
        Done.IsVisible = true;
    }

    private async void UpdateBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        var update = await UpdateCheck.CheckGitHubNewerVersion();
        if (update == 1)
        {
            var up = new UpdateDialog();
            await up.ShowDialog(this);
        }
    }

    private void SettingsBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        Menu.IsVisible = false;
        Settings.IsVisible = true;
    }

    private void BckSttngs_OnClick(object? sender, RoutedEventArgs e)
    {
        Settings.IsVisible = false;
        Menu.IsVisible = true;
    }

    private void SaveSttngs_OnClick(object? sender, RoutedEventArgs e)
    {
        var result = SettingsFile.UpdateSettings(SettingsTlBox.Text, SettingsPrBox.Text, SettingsRawsBox.Text,
            SettingsClRdBox.Text, SettingsTsBox.Text, SettingsQcBox.Text, PrefixTxt.Text);
        ErrorMessages.ToErrorMessage(result);
        OnError();
        SettingsFile.GetSettings();
    }
}