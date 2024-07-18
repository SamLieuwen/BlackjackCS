using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackjackCS
{
    public class Split : BlackJack
    {   
        //Split Class Global Variables
        public static int splitScore;
        public static int splitBet;
        public static bool splitGame;

        //Blackjack splithand main code
        public new static void Main(String[] args)
        {
            Decks.splitHand = new List<Card>();
            Decks.splitHandAceCount = 0;
            Decks.playerHandAceCount = 0;
            splitBet = bet;
            runGame = true;
            splitGame = true;
            
            Decks.splitHand.Add(Decks.playerHand[1]);
            splitScore += Decks.splitHand[0].cV;
            playerScore -= Decks.splitHand[0].cV;
            Decks.playerHand.RemoveAt(1);

            if (Decks.playerHand[0].card == "Ace") { Decks.playerHandAceCount++; }
            if (Decks.splitHand[0].card == "Ace") { Decks.splitHandAceCount++; }

            while (runGame)
            {
                Console.Clear();
                Console.Write("Bet: $" + bet + "\n1st Hand: ");
                foreach (Card card in Decks.playerHand)
                {
                    Console.Write(card.card + " ");
                }
                Console.Write("\n\nSplit Bet: $" + splitBet + "\n2nd Hand: ");
                foreach (Card card in Decks.splitHand)
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
                        SplitActions.FirstHandActions();
                    }
                    else
                    {
                        runGame = false;
                        Console.WriteLine("\nYour 1st Hand Bust \nPresplitScore 'Space' To Continue");
                        Console.ReadKey();
                        break;
                    }
                }
                else if (playerScore == 21)
                {
                    runGame = false;
                    Console.WriteLine("Your 1st Hand Got 21\nPress 'Space' To Continue");
                    Console.ReadKey();
                }
                else
                {
                    SplitActions.FirstHandActions();
                }
            }

            while (splitGame)
            {
                Console.Clear();
                Console.Write("Bet: $" + bet + "\n1st Hand: ");
                foreach (Card card in Decks.playerHand)
                {
                    Console.Write(card.card + " ");
                }
                Console.Write("\n\nSplit Bet: $" + splitBet + "\n2nd Hand: ");
                foreach (Card card in Decks.splitHand)
                {
                    Console.Write(card.card + " ");
                }
                Console.WriteLine("\n\nDealer Hand: " + Decks.dealerHand.First().card);

                if (splitScore > 21)
                {
                    if (Decks.splitHandAceCount > 0)
                    {
                        splitScore -= 10;
                        Decks.splitHandAceCount--;
                        SplitActions.SplitHandActions();
                    }
                    else
                    {
                        splitGame = false;
                        Console.WriteLine("\nYour 2nd Hand Bust\nPresplitScore 'Space' To Continue");
                        Console.ReadKey();

                        Results();
                        break;
                    }
                }
                else if (splitScore == 21)
                {
                    splitGame = false;
                    Console.WriteLine("\nYour 2nd Hand Got 21\nPresplitScore 'Space' To Continue");
                    Console.ReadKey();

                    SplitActions.SplitStand();
                }
                else
                {
                    SplitActions.SplitHandActions();
                }
            }

            Console.Write("\nTotal Earnings: $" + totalEarnings + "\nWould you like to play again: (Yes / No) ");
            if ("yes" == Console.ReadLine().ToLower())
            {
                Console.Clear();
                BlackJack.Main(null);
            }
        }

        //Multi-Hand Results
        public new static void Results()
        {
            Console.Clear();

            Console.Write("Bet: $" + bet + "\n1st Hand: ");
            foreach (Card card in Decks.playerHand)
            {
                Console.Write(card.card + " ");
            }
            Console.WriteLine();
            Console.Write("\nSplit Bet: $" + splitBet + "\n2nd Hand: ");
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

            if (dealerScore > 21 && playerScore <= 21 && splitScore <= 21) { Console.WriteLine("\nDealer Bust\nPayout: $" + ((bet * 2) + (splitBet * 2))); return; }

            if (playerScore > 21) { Console.WriteLine("\nYour 1st Hand Bust\nDealer Wins"); totalEarnings -= bet; }
            else if (dealerScore > 21) { Console.WriteLine("\nDealer Bust\nPayout: $" + bet * 2); totalEarnings += (bet * 2); }
            else if (playerScore == dealerScore) { Console.WriteLine("\nYour 1st was a Stand\nPayout: $" + bet); }
            else if (playerScore > dealerScore && playerScore <= 21) { Console.WriteLine("\nYour 1st Hand Won\nPayout: $" + bet * 2); totalEarnings += (bet * 2); }
            else if (dealerScore > playerScore && dealerScore <= 21) { Console.WriteLine("\nDealer Beat Your 1st Hand"); totalEarnings -= bet; }

            if (splitScore > 21) { Console.WriteLine("\nYour 2nd Hand Bust\nDealer Wins"); totalEarnings -= splitBet; }
            else if (dealerScore > 21) { Console.WriteLine("\nDealer Bust\nPayout: $" + splitBet * 2); totalEarnings += (splitBet * 2); }
            else if (splitScore == dealerScore) { Console.WriteLine("\nYour 2nd was a Stand\nPayout: $" + splitBet); }
            else if (splitScore > dealerScore && splitScore <= 21) { Console.WriteLine("\nYour 2nd Hand Won\nPayout: $" + splitBet * 2); totalEarnings += (splitBet * 2); }
            else if (dealerScore > splitScore && dealerScore <= 21) { Console.WriteLine("\nDealer Beat Your 2nd Hand"); totalEarnings -= splitBet; }
        }
    }
}
