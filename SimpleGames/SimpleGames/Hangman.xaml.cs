using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SimpleGames
{
    /// <summary>
    /// Logika interakcji dla klasy Hangman.xaml
    /// Change interactive and controls on window, mainly Front-end
    /// </summary>
    public partial class Hangman : Window
    {
        int mistake = 0,correct=0;
        List<int> letters = new List<int>();

        public Hangman()
        {
            InitializeComponent();
            LoadGame();
        }
  
        private void WinGame()
        {
            MessageBox.Show("Wygrałeś", "Koniec gry", MessageBoxButton.OK);
            NewGame();
        }

        private void LostGame()
        {
            CorrectAnswer();
            MessageBox.Show("Przegrana", "Koniec gry", MessageBoxButton.OK);
            NewGame();
        }

        private void NewGame()
        {
            if (MessageBox.Show("Czy chcesz rozpocząć nową grę?", "Koniec gry", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                mistake = 0;
                correct = 0;
                countmiss.Content = 7;
                words.Children.Clear();
                words.ColumnDefinitions.Clear();
                RandomWord();
            }
            else
            {
                this.Close();
            }
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (search.Text == "")
            {
                CheckLetter.IsEnabled = false;
                
            }
            else
            {
                CheckLetter.IsEnabled = true;
            }
        }

        private void Search_KeyUp(object sender, KeyEventArgs e)
        {    
            if (e.Key == Key.Enter && mistake<6)
            {
                    CheckLetter_Click(sender,e);
            }
        }

        private void CheckLetter_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(search.Text))
            {
                int info = CheckWord(search.Text.ToUpper());
                if (info == 0)
                {
                    ++mistake;
                    ChangePicture(mistake);
                }
                else
                {
                    correct += info;
                }

                ControlResult();
                search.Clear();
            }

        }

        private Label ChangeValue(int i, char c, bool loser = false)
        {
            Label label = new Label();
            label.Content = c;
            Grid.SetColumn(label, i);
            label.Style = Resources["letterStyle"] as Style;
            label.Name = "w" + i.ToString();
            if (loser != false)
            {
                label.Foreground = new SolidColorBrush(Colors.Red);
            }
            return label;
        }

        private ColumnDefinition ColumnDef()
        {
            ColumnDefinition column = new ColumnDefinition();
            column.Width = new GridLength(1, GridUnitType.Star);
            return column;
        }

        private Border BorderDef(int col)
        {
            Border border = new Border();
            border.Style = Resources["wordStyle"] as Style;
            Grid.SetColumn(border, col);
            return border;
        }
        
    }
}
