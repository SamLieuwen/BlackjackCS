using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackCS
{
    internal class Actions
    {
        public static void SingleHandActions()
        {
            string answer = "";
            bool loop = true;

            if (Decks.playerHand.Count < 3)
            {
                Console.WriteLine("\nWhat would you like to do: 'Hit' 'Stand' 'Double Down'");
                while (loop)
                {
                    answer = Console.ReadLine().ToLower();

                    switch (answer)
                    {
                        case "hit":
                            Hit();
                            loop = false;
                            break;
                        case "stand":
                            Stand();
                            loop = false;
                            break;
                        case "double down":
                            DoubleDown();
                            loop = false;
                            break;
                        default:
                            Console.WriteLine("Invalid Response");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("\nWhat would you like to do: 'Hit' 'Stand'");
                while (loop)
                {
                    answer = Console.ReadLine().ToLower();

                    switch (answer)
                    {
                        case "hit":
                            Hit();
                            loop = false;
                            break;
                        case "stand":
                            Stand();
                            loop = false;
                            break;
                        default:
                            Console.WriteLine("Invalid Response");
                            break;
                    }
                }
            }
        }

        public static void Hit()
        {
            Decks.playerHand.Add(Decks.deck[0]);
            Decks.deck.RemoveAt(0);

            BlackJack.playerScore += Decks.playerHand[Decks.playerHand.Count - 1].cV;

            if (Decks.playerHand[Decks.playerHand.Count - 1].card == "Ace") { Decks.playerHandAceCount++; }
            if (BlackJack.playerScore > 21 && Decks.playerHandAceCount > 0)
            {
                BlackJack.playerScore -= 10;
                Decks.playerHandAceCount--;
            }
        }
        public static void Stand()
        {
            BlackJack.runGame = false;

            while (true)
            {
                Console.Clear();
                Console.Write("Bet: $" + BlackJack.bet + "\nYour Hand: ");
                foreach (Card card in Decks.playerHand)
                {
                    Console.Write(card.card + " ");
                }
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
                    BlackJack.Results();
                    break;
                }
                else if (BlackJack.dealerScore > 21)
                {
                    BlackJack.Results();
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
        public static void DoubleDown()
        {
            BlackJack.bet *= 2;

            Hit();

            if (BlackJack.playerScore == 21) { Console.WriteLine("Your Hand Got 21!\nPress 'Space' to continue"); Console.ReadKey(); }

            Stand();
        }
        public static void SplitGame()
        {
            BlackJack.runGame = false;
            Split.Main(null);
        }
    }
}
