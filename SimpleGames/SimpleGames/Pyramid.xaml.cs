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
    /// Logika interakcji dla klasy Pyramid.xaml
    /// poprawić zmienianie się kart w dodatkowej talii po zabraniu
    /// method mainly about window Pyramid
    /// </summary>
    public partial class Pyramid : Window
    {
        private List<Grid> rows = new List<Grid>();
        private int whichCard = 0;
        private List<Button> buttons = new List<Button>();

        public Pyramid()
        {
            InitializeComponent();
            PrepareGame();
        }

        private void PrepareGame()
        {
            FindAllRow();
            FillListCards();
            CardsOnPlace();
        }

        private void FindAllRow()
        {
            foreach (var item in allgrid.Children)
            {
                if(item is Grid) // or item.GetType==typeof(Grid)
                {
                    rows.Add((Grid)item);
                }
            }
        }

        private void UnlockCard(int tmpRow,int tmpCol)
        {
            if(CheckIsButton()){         
            bool unlock = true;
            foreach (var item in rows[tmpRow-1].Children)
            {
                if(item is Button)
                {
                    Button button = (Button)item;
                    int sub = Convert.ToInt32(button.Name.Substring(1));
                    int countUnlock = 0;
                    foreach (var item2 in rows[tmpRow].Children)
                    {
                        if (item2 is Button)
                        {
                            Button button2 = (Button)item2;
                            string tmp2 = "" + (sub + 1);
                            if ((button2.Name == "w" + sub) || (button2.Name == "w" + (tmp2)))
                                ++countUnlock;
                        }
                    }
                    if (countUnlock==0)
                    {
                        button.IsEnabled = true;
                    }
                }
            }
            }
        }

        public Button CreateCard(int value,int row,int column)
        {
            Button button = new Button();
            button.Content = value;
            button.Name = "w" + column;
            if (row < 6)
            {
                button.IsEnabled = false;
            }
            button.Click += Hold_Click;
            Grid.SetRow(button, row);
            Grid.SetColumn(button, column);         
            return button;
        }

        private void Hold_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = Brushes.Sienna;
            if ((int)button.Content == 13)
            {
                DelButton(button);
            }
            else
            {
                buttons.Add(button);
                if (buttons.Count > 1)
                {
                    CheckSum();
                }
            }
        }

        private bool CheckIsButton()
        {
            bool isButton = false;
            foreach (var item in w0.Children)
            {
                if(item is Button)
                    isButton = true;              
            }
            if (isButton == false)
            {
                EndGame();
                return false;
            }
            else
            {
                return true;
            }
        }

        private void EndGame()
        {
            if (MessageBox.Show("Wygrałeś", "Gratulacje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Window window = new Pyramid();
                window.ShowDialog();
                this.Close();
            }
            else
                this.Close();
        }

        private void DelButton(Button but)
        {
            string tmpName = but.Name;
            int tmpRow = Grid.GetRow(but);
            int tmpCol = Grid.GetColumn(but);
            if (but.Name == "AddCard")
            {
                if (addcards.Count > 0)
                {
                    addcards.RemoveAt(whichCard);
                    but.Background = Brushes.Beige;
                    --whichCard;
                    if(whichCard<0 && whichCard<addcards.Count)
                        whichCard = 0;
                    but.Content = addcards[whichCard];
                }
                else
                    AddRow.Children.Remove(but);
            }
            else
            {
                int delrow = Grid.GetRow(but);
                rows[delrow].Children.Remove(but);
            }
            if(tmpName!="AddCard")
                UnlockCard(tmpRow,tmpCol);
        }

        private void CheckSum()
        {
            int b1 = (int)buttons[0].Content;
            int b2 = (int)buttons[1].Content;
            if (b1+b2==13)
            {
                DelButton(buttons[0]);
                DelButton(buttons[1]);
            }
            else
            {
                buttons[0].Background = Brushes.Beige;
                buttons[1].Background = Brushes.Beige;
            }
            buttons.Clear();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            ++whichCard;
            if (!(whichCard < addcards.Count))
                whichCard = 0;

            AddCard.Content = addcards[whichCard];
        }
    }
}
