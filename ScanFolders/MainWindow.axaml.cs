//TODO: Comment stuff you 

using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Platform;
using ScanFolders.Classes;
using ScanFolders.Views;
using ScanFolders.Classes;
using ScanFolders.Viewmodels;

namespace ScanFolders
{
    public partial class MainWindow : Window
    {
        int selection = 0;
        private static string path = null!;
        string amountString = null;
        string splitString = null;
        string bonusString = null;
        string beginString = null;
        private int bonusSel = 0;
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChBtn_OnClick(object? sender, RoutedEventArgs e)
        {
            selection = 1;
            Menu.IsVisible = false;
            Directory.IsVisible = true;
        }

        private void ScanBtn_OnClick(object? sender, RoutedEventArgs e)
        {
            //ErrorMessages.ToErrorMessage(38);
            //OnError();
            
            selection = 2;
            Menu.IsVisible = false;
            Directory.IsVisible = true;
            Sel2Lbl.IsVisible = true;
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
            if (CheckPermissions.IsDirectoryWritable(path) == true)
            {
                NxtDir.IsEnabled = true;
            }
            else
            {
                NxtDir.IsEnabled = false;
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
            Sel2Lbl.IsVisible = false;
            Menu.IsVisible = true;
        }
        
        private void NxtCh_OnClick(object? sender, RoutedEventArgs e)
        {
            var charTestS = AmountTxt.Text + StartChTxt.Text + SplitTxt.Text + BonusTxt.Text; //Combine content of textboxes
            var charTest = charTestS.ToCharArray();
            char[] bonusTest = null;
            if (BonusTxt.Text != null)
            {
                bonusTest = BonusTxt.Text.ToCharArray();
            }
            //AmountTxt.Text = charTest;
            if (charTest.Any(char.IsLetter) || charTest.Any(char.IsSymbol) || //Check if any of the textboxes have letters or symbols
                (charTest.Any(char.IsPunctuation) && bonusTest.Any(x => char.IsPunctuation(x) && x != ',')))
            {
                ErrorMessages.ToErrorMessage(44); //Set error code to 44 in ErrorMessages.cs
                OnError(); //Show Error dialog
            }
            else
            {
                if (AmountTxt.Text == "" || StartChTxt.Text == "" || SplitTxt.Text == "" ||
                    (bonusSel != 0 && BonusTxt.Text == ""))
                {
                    
                }
                else
                {
                    int splitInt = Convert.ToInt32(SplitTxt.Text);
                    int beginInt = Convert.ToInt32(StartChTxt.Text);
                    int amountInt = Convert.ToInt32(AmountTxt.Text);

                    ChaptersMenu.IsVisible = false;
                    LoadBar.IsVisible = true;
                    Chapters.CreateChapter(bonusSel, splitInt, beginInt, amountInt, path,BonusTxt.Text, TlChBox.IsChecked, PrChBox.IsChecked);
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

        private void OnError()
        {
            var ed = new ErrorDialog();
            ed.ShowDialog(this);
        }

        private void BonusButtonChecked(object? sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb != null)
            {
                string bonusSelection = rb.Tag.ToString();
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
        
        private static bool isValid(String str)
        {
            return Regex.IsMatch(str, @"^[a-zA-Z]+$");
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
            Folders.CreateFolders(path, TlBox.IsChecked, PrBox.IsChecked, RawsBox.IsChecked, ClrdBox.IsChecked, TsBox.IsChecked, QcBox.IsChecked);
            LoadBar.IsVisible = false;
            Done.IsVisible = true;
        }
    }
}