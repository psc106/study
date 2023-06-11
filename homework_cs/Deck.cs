using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_cs
{
    class Deck
    {
        private Card[] cards;
        private int size;


        public Deck(int size) {
            cards = new Card[size];
            this.size = size;
            for (int i = 0; i < size; i++)
            {
                cards[i] = new Card(i % 13, i / 13);
            }
        }
        public Deck()
        {
            cards = new Card[52];
            this.size = 52;
            for (int i = 0; i < 52; i++)
            {

                cards[i] = new Card(-1, -1);
            }
        }

        public int GetSize() { return size; }


        public void Swap()
        {
            Random rand = new Random();
            int randNum1 = 0;
            int randNum2 = 0;
            for (int i = 0; i < 300; i++)
            {
                randNum1 = rand.Next(0, 52);
                randNum2 = rand.Next(0, 52);

                Card tmp = cards[randNum1];
                cards[randNum1] = cards[randNum2];
                cards[randNum2] = tmp;
            }
        }

        public String GetCardString(int num)
        {
            return cards[num].Pattern+cards[num].Number;
        }

        public Card Draw(int index)
        {
            Card card = cards[index];
            cards[index] = new Card();
            return card;
        }

        public void Discard(Deck srcDeck, Deck dstDeck, int index)
        {
            dstDeck.cards[index] = srcDeck.cards[index];
            srcDeck.cards[index] = new Card();

        }
    }
}
