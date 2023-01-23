//TODO: Comment stuff you

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using ScanFolders.Classes;
using ScanFolders.Viewmodels;
using ScanFolders.Views;

//Stop ReSharper from crying about the buttons' unused parameters
// ReSharper disable UnusedParameter.Local

namespace ScanFolders;

public partial class MainWindow : Window
{
    private static string _path = null!;
    private int bonusSel;
    private int selection;
    private double screenHeight;
    private double screenWidth;

    public MainWindow()
    {
        InitializeComponent();

        var vm = new MainWindowViewModel();
        DataContext = vm;
        
        MenuButtonSize();
        GetSize();
        //SetWindowSize();
        
    }
    
    /// <summary>
    /// Scale main buttons to the largest button
    /// </summary>
    private void MenuButtonSize()
    {
        //Get button sizes
        //TODO: This gives zero, fix
        ChBtn.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        ScanBtn.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        SettingsBtn.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        ExitBtn.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

        ChBtn.DesiredSize.Deconstruct(out var chBtnWidth, out var chBtnHeight);
        ScanBtn.DesiredSize.Deconstruct(out var scanBtnWidth, out var scanBtnHeight);
        SettingsBtn.DesiredSize.Deconstruct(out var settingsBtnWidth, out var settingsBtnHeight);
        ExitBtn.DesiredSize.Deconstruct(out var exitBtnWidth, out var exitBtnHeight);

        double[] findWidth =
        {
            chBtnWidth,
            scanBtnWidth,
            settingsBtnWidth,
            exitBtnWidth,
        };

        double[] findHeight =
        {
            chBtnHeight,
            scanBtnHeight,
            settingsBtnHeight,
            exitBtnHeight,
        };

        var highestWidth = findWidth.Max();
        var highestHeight = findHeight.Max();

        foreach (var VARIABLE in findWidth)
        {
            Console.WriteLine(VARIABLE);
        }
        Console.WriteLine(highestWidth + "\n");
        
        
        ChBtn.Width = ScanBtn.Width = SettingsBtn.Width = ExitBtn.Width = highestWidth;
        ChBtn.Height = ScanBtn.Height = SettingsBtn.Height = ExitBtn.Height = highestHeight;
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
        _path = result;
        DirText.Text = _path;
        if (CheckPermissions.IsDirectoryWritable(_path))
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
        char[] bonusTest = null!;
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
                var even = 0;
                if (amountInt % 2 != 0) even = 1;

                ChaptersMenu.IsVisible = false;
                LoadBar.IsVisible = true; //TODO: Does not actually show?
                
                //Unnecessary multithreading that's only noticeable when running older hardware or making a lot of folders
                var thr1 = new Thread(()=>Chapters.CreateChapter(bonusSel, splitInt, beginInt,
                    (amountInt / 2), _path, BonusTxt.Text!, TlChBox.IsChecked, PrChBox.IsChecked));
                var thr2 = new Thread(()=>Chapters.CreateChapter(bonusSel, splitInt, beginInt + (amountInt / 2),
                    (amountInt / 2) + even, _path, BonusTxt.Text!, TlChBox.IsChecked, PrChBox.IsChecked));

                thr1.Start();
                thr2.Start();

                thr1.Join();
                thr2.Join();
                
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

    /// <summary>
    /// Show error dialog
    /// </summary>
    private void OnError()
    {
        var ed = new ErrorDialog();
        ed.ShowDialog(this);
    }

    private void BonusButtonChecked(object? sender, RoutedEventArgs e)
    {
        if (sender is not RadioButton rb) return;
        var bonusSelection = rb.Tag?.ToString();
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

    private void MenuDir_OnClick(object? sender, RoutedEventArgs e)
    {
        Done.IsVisible = false;
        Menu.IsVisible = true;
    }

    private void NxtFolder_OnClick(object? sender, RoutedEventArgs e)
    {
        Folder.IsVisible = false;
        LoadBar.IsVisible = true;
        Folders.CreateFolders(_path, TlBox.IsChecked, PrBox.IsChecked, RawsBox.IsChecked, ClrdBox.IsChecked,
            TsBox.IsChecked, QcBox.IsChecked);
        LoadBar.IsVisible = false;
        Done.IsVisible = true;
    }

    private async void UpdateBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        var update = await UpdateCheck.CheckGitHubNewerVersion();
        if (update != 1) return;
        var up = new UpdateDialog();
        await up.ShowDialog(this);
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
    
    /// <summary>
    /// Get size of the screen
    /// </summary>
    private void GetSize()
    {
        var window = this.GetSelfAndLogicalAncestors().OfType<Window>().First();
        var screen = window.Screens.ScreenFromPoint(Position);

        screenHeight = screen!.Bounds.Size.Height;
        screenWidth = screen.Bounds.Size.Width;
    }

    // private void SetWindowSize()
    // {
    //     Window.Width = screenWidth / 2;
    //     Window.Height = screenHeight / 2;
    // }
}