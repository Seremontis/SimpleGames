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
    /// Hide parts Hangman.xaml.cs, mainly back-end window
    /// 
    /// TODO: SCALE Randoms
    /// </summary>
    public partial class Hangman
    {
        private XML xML=new XML();
        private string word;

        private void RandomWord(int? i = null)
        {
            int rand = 0;
            do
            {
                Random random = new Random();
                rand = random.Next(0, xML.CategoryCount);
            } while (rand == i);
            CreateGame(rand);
            ChangePicture(0);
        }

        public void CreateGame(int rand)
        {
            word = xML.ReadWord(rand);
            for (int i = 0; i < word.Length; i++)
            {
                words.ColumnDefinitions.Add(ColumnDef());
                words.Children.Add(BorderDef(i));
            }
        }
        
        private void LoadGame()
        {
            RandomWord();
            CheckLetter.IsEnabled = false;
        }

        private void ControlResult()
        {
            if (mistake > 5)
            {
                LostGame();
            }
            else if (correct == word.Length)
            {
                WinGame();
            }
        }

        public int CheckWord(string letterinput)
        {
            char letter = letterinput[0];
            int correctmany = 0;
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == letter)
                {
                    words.Children.Add(ChangeValue(i, letter));
                    ++correctmany;
                }
            }         
            return correctmany;          
        }

        private void ChangePicture(int mistake)
        {
            string result = "w" + mistake;
            if (mistake > 0)
            {
                hangPic.Source = (ImageSource)Resources[result];
            }
            else
            {
                hangPic.Source = null;
            }
            int miss = Convert.ToInt32(countmiss.Content);
            --miss;
            countmiss.Content = miss;

        }

        private void CorrectAnswer()
        {
            foreach (var item in words.Children)
            {
                if (item is Label)
                {
                    Label label = (Label)item;
                    letters.Add(Convert.ToInt16(label.Name.Substring(1)));
                }
            }
            FillCorrect();
        }

        private void FillCorrect()
        {
            bool flag = false;
            for (int i = 0; i < word.Length; i++)
            {
                foreach (var item in letters)
                {
                    if (item == i)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag == false)
                    words.Children.Add(ChangeValue(i, word[i], true));

                flag = false;
            }
        }

    }
}
