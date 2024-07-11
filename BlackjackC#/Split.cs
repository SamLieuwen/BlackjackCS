using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackjackCS
{
    public class Split : BlackJack
    {   
        //Split Class Global Variables
        private static List<Card> sH;
        private static int sS;
        private static int splitBet;
        private static bool splitGame;
        private static int sHAceCount;

        //Blackjack splithand main code
        public new static void Main(String[] args)
        {
            Split game = new Split();

            sH = new List<Card>();
            splitBet = bet;
            runGame = true;
            splitGame = true;
            pHAceCount = 0;
            sHAceCount = 0;

            sH.Add(pH[1]);
            sS += sH[0].cV;
            pS -= sH[0].cV;
            pH.RemoveAt(1);

            if (pH[0].card == "Ace") { pHAceCount++; }
            if (sH[0].card == "Ace") { sHAceCount++; }

            while (runGame)
            {
                Console.Clear();
                Console.Write("Bet: $" + bet + "\n1st Hand: ");
                foreach (Card card in pH)
                {
                    Console.Write(card.card + " ");
                }
                Console.Write("\n\nSplit Bet: $" + splitBet + "\n2nd Hand: ");
                foreach (Card card in sH)
                {
                    Console.Write(card.card + " ");
                }
                Console.WriteLine("\n\nDealer Hand: " + dH.First().card);

                if (pS > 21)
                {
                    if (pHAceCount > 0)
                    {
                        pS -= 10;
                        pHAceCount--;
                        game.Actions(game);
                    }
                    else
                    {
                        runGame = false;
                        Console.WriteLine("\nYour 1st Hand Bust \nPress 'Space' To Continue");
                        Console.ReadKey();
                        break;
                    }
                }
                else if (pS == 21)
                {
                    runGame = false;
                    Console.WriteLine("Your 1st Hand Got 21\nPress 'Space' To Continue");
                    Console.ReadKey();
                }
                else
                {
                    game.Actions(game);
                }
            }

            while (splitGame)
            {
                Console.Clear();
                Console.Write("Bet: $" + bet + "\n1st Hand: ");
                foreach (Card card in pH)
                {
                    Console.Write(card.card + " ");
                }
                Console.Write("\n\nSplit Bet: $" + splitBet + "\n2nd Hand: ");
                foreach (Card card in sH)
                {
                    Console.Write(card.card + " ");
                }
                Console.WriteLine("\n\nDealer Hand: " + dH.First().card);

                if (sS > 21)
                {
                    if (sHAceCount > 0)
                    {
                        sS -= 10;
                        sHAceCount--;
                        game.SplitActions(game);
                    }
                    else
                    {
                        splitGame = false;
                        Console.WriteLine("\nYour 2nd Hand Bust\nPress 'Space' To Continue");
                        Console.ReadKey();

                        Split.Results();
                        break;
                    }
                }
                else if (sS == 21)
                {
                    splitGame = false;
                    Console.WriteLine("\nYour 2nd Hand Got 21\nPress 'Space' To Continue");
                    Console.ReadKey();

                    game.Stand();
                }
                else
                {
                    game.SplitActions(game);
                }
            }

            Console.Write("\nTotal Earnings: $" + totalEarnings + "\nWould you like to play again: (Yes / No) ");
            if ("yes" == Console.ReadLine().ToLower())
            {
                Console.Clear();
                BlackJack.Main(null);
            }
        }

        //Action Distribution for both hands
        public void Actions(Split game)
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
                        game.Hit();
                        loop = false;
                        break;
                    case "stand":
                        runGame = false;
                        loop = false;
                        break;
                    case "double down":
                        game.DoubleD(game);
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Response");
                        break;
                }
            }
        }
        public void SplitActions(Split game)
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
                        game.SplitHit();
                        loop = false;
                        break;
                    case "stand":
                        game.Stand();
                        loop = false;
                        break;
                    case "double down":
                        game.SplitDD(game);
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Response");
                        break;
                }
            }
        }
        
        //Player actions for both hands
        public new void Hit()
        {
            pH.Add(deck[0]);
            deck.RemoveAt(0);

            pS += pH[pH.Count - 1].cV;

            if (pH[pH.Count - 1].card == "Ace") { pHAceCount++; }
            if (pS > 21 && pHAceCount > 0)
            {
                pS -= 10;
                pHAceCount--;
            }
        }
        public void SplitHit()
        {
            sH.Add(deck[0]);
            deck.RemoveAt(0);

            sS += sH[sH.Count - 1].cV;

            if (sH[sH.Count - 1].card == "Ace") { sHAceCount++; }
            if (sS > 21 && sHAceCount > 0)
            {
                sS -= 10;
                sHAceCount--;
            }
        }
        public void DoubleD(Split game)
        {
            bet *= 2;
            game.Hit();
            runGame = false;
        }
        public void SplitDD(Split game)
        {
            splitBet *= 2;
            game.SplitHit();
            game.Stand();
        }
        public new void Stand()
        {
            splitGame = false;

            while (true)
            {
                Console.Clear();
                Console.Write("Bet: $" + bet + "\n1st Hand: ");
                foreach (Card card in pH)
                {
                    Console.Write(card.card + " ");
                }
                Console.WriteLine();
                Console.Write("\nSplit Bet: $" + splitBet + "\n2nd Hand: ");
                foreach (Card card in sH)
                {
                    Console.Write(card.card + " ");
                }
                Console.WriteLine();
                Console.Write("\nDealer Hand: ");
                foreach (Card card in dH)
                {
                    Console.Write(card.card + " ");
                }
                Console.WriteLine();

                if (dS > 21 && dHAceCount > 0)
                {
                    dS -= 10;
                    dHAceCount--;
                }

                if (dS >= 17 && dS <= 21)
                {
                    Split.Results();
                    break;
                }
                else if (dS > 21)
                {
                    Split.Results();
                    break;
                }
                else
                {
                    dH.Add(deck[0]);
                    deck.RemoveAt(0);

                    dS += dH[dH.Count - 1].cV;

                    if (dH[dH.Count - 1].card == "Ace") { dHAceCount++; }
                }
            }
        }

        //Multi-hand results
        public new static void Results()
        {
            Console.Clear();

            Console.Write("Bet: $" + bet + "\n1st Hand: ");
            foreach (Card card in pH)
            {
                Console.Write(card.card + " ");
            }
            Console.WriteLine();
            Console.Write("\nSplit Bet: $" + splitBet + "\n2nd Hand: ");
            foreach (Card card in sH)
            {
                Console.Write(card.card + " ");
            }
            Console.WriteLine();
            Console.Write("\nDealer Hand: ");
            foreach (Card card in dH)
            {
                Console.Write(card.card + " ");
            }
            Console.WriteLine();

            if (dS > 21 && pS <= 21 && sS <= 21) { Console.WriteLine("\nDealer Bust\nPayout: $" + ((bet * 2) + (splitBet * 2))); return; }

            if (pS > 21) { Console.WriteLine("\nYour 1st Hand Bust\nDealer Wins"); totalEarnings -= bet; }
            else if (dS > 21) { Console.WriteLine("\nDealer Bust\nPayout: $" + bet * 2); totalEarnings += (bet * 2); }
            else if (pS == dS) { Console.WriteLine("\nYour 1st was a Stand\nPayout: $" + bet); }
            else if (pS > dS && pS <= 21) { Console.WriteLine("\nYour 1st Hand Won\nPayout: $" + bet * 2); totalEarnings += (bet * 2); }
            else if (dS > pS && dS <= 21) { Console.WriteLine("\nDealer Beat Your 1st Hand"); totalEarnings -= bet; }

            if (sS > 21) { Console.WriteLine("\nYour 2nd Hand Bust\nDealer Wins"); totalEarnings -= splitBet; }
            else if (dS > 21) { Console.WriteLine("\nDealer Bust\nPayout: $" + splitBet * 2); totalEarnings += (splitBet * 2); }
            else if (sS == dS) { Console.WriteLine("\nYour 2nd was a Stand\nPayout: $" + splitBet); }
            else if (sS > dS && sS <= 21) { Console.WriteLine("\nYour 2nd Hand Won\nPayout: $" + splitBet * 2); totalEarnings += (splitBet * 2); }
            else if (dS > sS && dS <= 21) { Console.WriteLine("\nDealer Beat Your 2nd Hand"); totalEarnings -= splitBet; }
        }
    }
}
