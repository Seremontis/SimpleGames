using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGames
{
    /// <summary>
    ///  method mainly about cards
    /// </summary>
    public partial class Pyramid
    {
        private List<Cards> cards = new List<Cards>();
        private List<int> addcards = new List<int>();
        private Random random = new Random();

        private void FillListCards()
        {
            for (int i = 1; i <= 13; i++)
            {
                cards.Add(new Cards() { Name = i, Count = 4 });
            }
        }

        private void CardsOnPlace()
        {
            for (int row = 0; row <= 6; row++)
            {
                for (int column = 0; column <= row; column++)
                {
                    rows[row].Children.Add(CreateCard(RandomCard(), row, column));
                }
            }
            FillOtherCards();
            AddCard.Content = addcards[whichCard];
        }

        private void FillOtherCards()
        {
            do
            {
                addcards.Add(RandomCard());
            } while (cards.Count > 0);
        }

        private int RandomCard()
        {
            int tmp = random.Next(0, cards.Count);
            int result = cards[tmp].Name;
            DeleteCardsOfStack(tmp);
            return result;
        }

        private void DeleteCardsOfStack(int tmp)
        {
            cards[tmp].Count--;
            if (cards[tmp].Count < 1)
            {
                cards.RemoveAt(tmp);
            }
        }
    }
}
