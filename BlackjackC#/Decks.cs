using BlackjackCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackCS
{
    internal class Decks
    {
        public static List<Card> deck;
        public static List<Card> playerHand;
        public static List<Card> splitHand;
        public static List<Card> dealerHand;
        
        public static int playerHandAceCount;
        public static int splitHandAceCount;
        public static int dealerHandAceCount;

        public static void CreateDeck()
        {
            for (int i = 0; i < 4; i++)
            {
                deck.Add(new Card("Ace", 11));
                deck.Add(new Card("Two", 2));
                deck.Add(new Card("Three", 3));
                deck.Add(new Card("Four", 4));
                deck.Add(new Card("Five", 5));
                deck.Add(new Card("Six", 6));
                deck.Add(new Card("Seven", 7));
                deck.Add(new Card("Eight", 8));
                deck.Add(new Card("Nine", 9));
                deck.Add(new Card("Ten", 10));
                deck.Add(new Card("Jack", 10));
                deck.Add(new Card("Queen", 10));
                deck.Add(new Card("King", 10));
            }

            var shuffled = new List<Card>();
            var rand = new Random();

            while (deck.Count != 0)
            {
                var i = rand.Next(deck.Count);
                shuffled.Add(deck[i]);
                deck.RemoveAt(i);
            }
            deck = shuffled;
        }
    }
}
