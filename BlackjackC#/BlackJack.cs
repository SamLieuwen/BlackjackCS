using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackjackCS
{
    public class BlackJack
    {
        //Universal Variables
        public static double totalEarnings = 0.0;
        public static int playerScore;
        public static int dealerScore;
        public static int bet;
        public static bool runGame;

        //Blackjack main code
        public static void Main(String[] args)
        {
            Decks.deck = new List<Card>();
            Decks.playerHand = new List<Card>();
            Decks.dealerHand = new List<Card>();
            Decks.playerHandAceCount = 0;
            Decks.dealerHandAceCount = 0;
            playerScore = 0;
            dealerScore = 0;
            bet = 0;
            runGame = true;

            Decks.CreateDeck();

            Console.WriteLine("Welcome to Blackjack!");

            while (true)
            {
                try
                {
                    Console.Write("Place your bet: $");
                    bet = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid Response");
                }
            }

            for (int i = 0; i < 2; i++)
            {
                Decks.playerHand.Add(Decks.deck[0]);
                Decks.deck.RemoveAt(0);
                Decks.dealerHand.Add(Decks.deck[0]);
                Decks.deck.RemoveAt(0);
            }

            foreach (Card card in Decks.playerHand)
            {
                if (card.card == "Ace") { Decks.playerHandAceCount++; }
                playerScore += card.cV;
            }
            foreach (Card card in Decks.dealerHand)
            {
                if (card.card == "Ace") { Decks.dealerHandAceCount++; }
                dealerScore += card.cV;
            }

            if (Decks.playerHand.Count == 2 && playerScore == 21)
            {
                runGame = false;
                totalEarnings += Convert.ToDouble(bet) * 1.5;

                Console.Clear();
                Console.Write("Bet: $" + bet + "\nYour Hand: ");
                foreach (Card card in Decks.playerHand)
                {
                    Console.Write(card.card + " ");
                }
                Console.WriteLine();
                Console.WriteLine("\nDealer Hand: " + Decks.dealerHand.First().card);
                Console.WriteLine("\nBlackJack\nPayout: $" + Convert.ToDouble(bet) * 1.5);
            }
            if (Decks.dealerHand.Count == 2 && dealerScore == 21)
            {
                totalEarnings -= bet;
                
                runGame = false;
                Console.Clear();
                Console.Write("Bet: $" + bet + "\nYour Hand: ");
                foreach (Card card in Decks.playerHand)
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
                Console.WriteLine("\nDealer got BlackJack");
            }

            if (Decks.playerHand.Count == 2 && Decks.playerHand[0].card == Decks.playerHand[1].card)
            {
                string answer = "";
                bool loop = true;

                Console.Clear();
                Console.Write("Bet: $" + bet + "\nYour Hand: ");
                foreach (Card card in Decks.playerHand)
                {
                    Console.Write(card.card + " ");
                }
                Console.WriteLine("\n\nDealer Hand: " + Decks.dealerHand.First().card);

                Console.WriteLine("\nWhat would you like to do: 'Hit' 'Stand' 'Double Down' 'Split'");
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
                            Actions.Stand();
                            loop = false;
                            break;
                        case "double down":
                            Actions.DoubleDown();
                            loop = false;
                            break;
                        case "split":
                            Actions.SplitGame();
                            loop = false;
                            break;
                        default:
                            Console.WriteLine("Invalid Response");
                            break;
                    }
                }
            }

            while (runGame)
            {
                Console.Clear();
                Console.Write("Bet: $" + bet + "\nYour Hand: ");
                foreach (Card card in Decks.playerHand)
                {
                    Console.Write(card.card + " ");
                }
                Console.WriteLine("\n\nDealer Hand: " + Decks.dealerHand.First().card);

                if (playerScore > 21)
                {
                    if (Decks.playerHandAceCount > 0)
                    {
                        playerScore -= 10;
                        Decks.playerHandAceCount--;
                        Actions.SingleHandActions();
                    }
                    else
                    {
                        runGame = false;
                        Console.WriteLine("\nYour Hand Bust\nPress 'Space' To Continue");
                        Console.ReadKey();
                        Results();
                        break;
                    }
                }
                else if (playerScore == 21)
                {
                    runGame = false;
                    Console.WriteLine("\nYour Hand Got 21\nPress 'Space' To Continue");
                    Console.ReadKey();
                    Actions.Stand();
                }
                else
                {
                    Actions.SingleHandActions();
                }
            }

            Console.Write("\nTotal Earnings: $" + totalEarnings + "\nWould you like to play again: (Yes / No) ");
            if ("yes" == Console.ReadLine().ToLower())
            {
                Console.Clear();
                Main(null);
            }
        }

        //Single-hand results
        public static void Results()
        {
            Console.Clear();

            Console.Write("Bet: $" + bet + "\nYour Hand: ");
            foreach (Card card in Decks.playerHand)
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

            if (playerScore > 21) { Console.WriteLine("\nYour Hand Bust\nDealer Wins"); totalEarnings -= bet; }
            else if (dealerScore > 21) { Console.WriteLine("\nDealer Bust\nPayout: $" + bet * 2); totalEarnings += (bet * 2); }
            else if (playerScore == dealerScore) { Console.WriteLine("\nYour was a Stand\nPayout: $" + bet); }
            else if (playerScore > dealerScore && playerScore <= 21) { Console.WriteLine("\nYour Hand Won\nPayout: $" + bet * 2); totalEarnings += (bet * 2);  }
            else if (dealerScore > playerScore && dealerScore <= 21) { Console.WriteLine("\nDealer Beat Your Hand"); totalEarnings -= bet; }
        }
    }
}
