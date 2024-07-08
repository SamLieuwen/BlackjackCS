﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackC_
{
    public class Split : BlackJack
    {   
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
                Console.WriteLine();
                Console.Write("\nSplit Bet: $" + splitBet + "\n2nd Hand: ");
                foreach (Card card in sH)
                {
                    Console.Write(card.card + " ");
                }
                Console.WriteLine();
                Console.WriteLine("\nDealer Hand: " + dH.First().card);

                if (pS > 21)
                {
                    if (pHAceCount > 0)
                    {
                        pS -= 10;
                        pHAceCount--;
                        game.actions(game);
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
                    game.actions(game);
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
                Console.WriteLine();
                Console.Write("\nSplit Bet: $" + splitBet + "\n2nd Hand: ");
                foreach (Card card in sH)
                {
                    Console.Write(card.card + " ");
                }
                Console.WriteLine();
                Console.WriteLine("\nDealer Hand: " + dH.First().card);

                if (sS > 21)
                {
                    if (sHAceCount > 0)
                    {
                        sS -= 10;
                        sHAceCount--;
                        game.splitActions(game);
                    }
                    else
                    {
                        splitGame = false;
                        Console.WriteLine("\nYour 2nd Hand Bust\nPress 'Space' To Continue");
                        Console.ReadKey();

                        Split.results();
                        break;
                    }
                }
                else if (sS == 21)
                {
                    splitGame = false;
                    Console.WriteLine("\nYour 2nd Hand Got 21\nPress 'Space' To Continue");
                    Console.ReadKey();

                    game.stand();
                }
                else
                {
                    game.splitActions(game);
                }
            }

            Console.Write("\nWould you like to play again: (Yes / No) ");
            if ("yes" == Console.ReadLine().ToLower())
            {
                Console.Clear();
                BlackJack.Main(null);
            }
        }

        //Action Distribution for both hands
        public void actions(Split game)
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
                        game.hit();
                        loop = false;
                        break;
                    case "stand":
                        runGame = false;
                        loop = false;
                        break;
                    case "double down":
                        game.doubleD(game);
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Response");
                        break;
                }
            }
        }
        public void splitActions(Split game)
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
                        game.splitHit();
                        loop = false;
                        break;
                    case "stand":
                        game.stand();
                        loop = false;
                        break;
                    case "double down":
                        game.splitDD(game);
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Response");
                        break;
                }
            }
        }
        
        //Player actions for both hands
        public new void hit()
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
        public void splitHit()
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
        public void doubleD(Split game)
        {
            bet *= 2;
            game.hit();
            runGame = false;
        }
        public void splitDD(Split game)
        {
            splitBet *= 2;
            game.splitHit();
            game.stand();
        }
        public new void stand()
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
                    Split.results();
                    break;
                }
                else if (dS > 21)
                {
                    Split.results();
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

        public new static void results()
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

            if (pS > 21) { Console.WriteLine("\nYour 1st Hand Bust\nDealer Wins"); }
            else if (dS > 21) { Console.WriteLine("\nDealer Bust\nPayout: $" + bet * 2); }
            else if (pS == dS) { Console.WriteLine("\nYour 1st was a Stand\nPayout: $" + bet); }
            else if (pS > dS && pS <= 21) { Console.WriteLine("\nYour 1st Hand Won\nPayout: $" + bet * 2); }
            else if (dS > pS && dS <= 21) { Console.WriteLine("\nDealer Beat Your 1st Hand"); }

            if (sS > 21) { Console.WriteLine("\nYour 2nd Hand Bust\nDealer Wins"); }
            else if (dS > 21) { Console.WriteLine("\nDealer Bust\nPayout: $" + splitBet * 2); }
            else if (sS == dS) { Console.WriteLine("\nYour 2nd was a Stand\nPayout: $" + splitBet); }
            else if (sS > dS && sS <= 21) { Console.WriteLine("\nYour 2nd Hand Won\nPayout: $" + splitBet * 2); }
            else if (dS > sS && dS <= 21) { Console.WriteLine("\nDealer Beat Your 2nd Hand"); }
        }
    }
}
