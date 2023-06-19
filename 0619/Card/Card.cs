using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0619.Card
{
    public class Card
    {
        static private readonly string[] _STRING_CARD_PATTERN = { "♣", "♥", "◆", "♠" };
        static private readonly string[] _STRING_CARD_NUMBER = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
        public int cardNumber { get; private set; }
        public bool isSelect { get; set; }

        public Card(int number)
        {
            this.isSelect = false;
            this.cardNumber = number;
        }
        public int GetCardPatternNum()
        {
            return (int)(this.cardNumber / 13);
        }

        public int GetCardNumberNum()
        {
            return (int)(this.cardNumber % 13);
        }

        public string GetCardString()
        {
            return GetCardPattern()+GetCardNumber();
        }

        public string GetCardPattern()
        {
            return _STRING_CARD_PATTERN[(int)(this.cardNumber / 13)];
        }

        public string GetCardNumber()
        {
            return _STRING_CARD_NUMBER[(int)(this.cardNumber % 13)];
        }

    }
}
