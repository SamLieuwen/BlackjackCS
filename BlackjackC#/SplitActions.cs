using BlackjackCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackCS
{
    internal class SplitActions
    {
        public static void FirstHandActions()
        {
            string answer = "";
            bool loop = true;

            Console.WriteLine("\nWhat would you like to do with your 1st Hand: 'Hit' 'Stand' 'Double Down'");
            while (loop)
            {
                answer = Console.ReadLine().ToLower();

                switch (answer)
                {
                    case "hit":
                        Actions.Hit();
                        loop = false;
                        break;
                    case "stand":
                        BlackJack.runGame = false;
                        loop = false;
                        break;
                    case "double down":
                        Actions.DoubleD();
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Response");
                        break;
                }
            }
        }
        public static void SplitHandActions()
        {
            string answer = "";
            bool loop = true;

            Console.WriteLine("\nWhat would you like to do with your 2nd Hand: 'Hit' 'Stand' 'Double Down'");
            while (loop)
            {
                answer = Console.ReadLine().ToLower();

                switch (answer)
                {
                    case "hit":
                        SplitHit();
                        loop = false;
                        break;
                    case "stand":
                        SplitStand();
                        loop = false;
                        break;
                    case "double down":
                        SplitDoubleDown();
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Response");
                        break;
                }
            }
        }

        public static void SplitHit()
        {
            Decks.splitHand.Add(Decks.deck[0]);
            Decks.deck.RemoveAt(0);

            Split.splitScore += Decks.splitHand[Decks.splitHand.Count - 1].cV;

            if (Decks.splitHand[Decks.splitHand.Count - 1].card == "Ace") { Decks.splitHandAceCount++; }
            if (Split.splitScore > 21 && Decks.splitHandAceCount > 0)
            {
                Split.splitScore -= 10;
                Decks.splitHandAceCount--;
            }
        }
        public static void SplitDoubleDown()
        {
            Split.splitBet *= 2;
            SplitHit();
            SplitStand();
        }
        public static void SplitStand()
        {
            Split.splitGame = false;

            while (true)
            {
                Console.Clear();
                Console.Write("Bet: $" + BlackJack.bet + "\n1st Hand: ");
                foreach (Card card in Decks.playerHand)
                {
                    Console.Write(card.card + " ");
                }
                Console.WriteLine();
                Console.Write("\nSplit Bet: $" + Split.splitBet + "\n2nd Hand: ");
                foreach (Card card in Decks.splitHand)
                {
                    Console.Write(card.card + " ");
                }
                Console.WriteLine();
                Console.Write("\nDealer Hand: ");
                foreach (Card card in Decks.dealerHand)
                {
                    Console.Write(card.card + " ");
                }
                Console.WriteLine();

                if (BlackJack.dealerScore > 21 && Decks.dealerHandAceCount > 0)
                {
                    BlackJack.dealerScore -= 10;
                    Decks.dealerHandAceCount--;
                }

                if (BlackJack.dealerScore >= 17 && BlackJack.dealerScore <= 21)
                {
                    Split.Results();
                    break;
                }
                else if (BlackJack.dealerScore > 21)
                {
                    Split.Results();
                    break;
                }
                else
                {
                    Decks.dealerHand.Add(Decks.deck[0]);
                    Decks.deck.RemoveAt(0);

                    BlackJack.dealerScore += Decks.dealerHand[Decks.dealerHand.Count - 1].cV;

                    if (Decks.dealerHand[Decks.dealerHand.Count - 1].card == "Ace") { Decks.dealerHandAceCount++; }
                }
            }
        }
    }
}
