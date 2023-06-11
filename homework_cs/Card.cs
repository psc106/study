using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_cs
{

    class Card
    {
        private readonly string[] CARD_PATTERN = {"◆","♥","♣","♠"};
        private readonly string[] CARD_NUMBER = {"A","2","3","4","5","6","7","8","9","10","J","Q","K"};

        private int number;
        private int pattern;

        public Card(int number, int pattern)
        {
            this.number = number;  
            this.pattern = pattern;
        }
        public Card()
        {
            this.number = -1;
            this.pattern = -1;
        }

        public string Number { get { return CARD_NUMBER[Number1]; }}
        public string Pattern { get { return CARD_PATTERN[Pattern1]; } }
        public int Number1 { get => number; set => number = value; }
        public int Pattern1 { get => pattern; set => pattern = value; }
    }
}
