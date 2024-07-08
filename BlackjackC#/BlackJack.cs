using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackjackC_
{
    public class BlackJack
    {
        //Universal Variables
        public static double totalEarnings = 0;
        public static List<Card> deck;
        public static List<Card> pH;
        public static List<Card> dH;
        public static int pS;
        public static int dS;
        public static int bet;
        public static int pHAceCount;
        public static int dHAceCount;
        public static bool runGame;

        //Blackjack main code
        public static void Main(String[] args)
        {
            BlackJack game = new BlackJack();

            deck = new List<Card>();
            pH = new List<Card>();
            dH = new List<Card>();
            pS = 0;
            dS = 0;
            bet = 0;
            pHAceCount = 0;
            dHAceCount = 0;
            runGame = true;

            BlackJack.createDeck();

            Console.WriteLine("Welcome To Blackjack!");

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
                pH.Add(deck[0]);
                deck.RemoveAt(0);
                dH.Add(deck[0]);
                deck.RemoveAt(0);
            }

            foreach (Card card in pH)
            {
                if (card.card == "Ace") { pHAceCount++; }
                pS += card.cV;
            }
            foreach (Card card in dH)
            {
                if (card.card == "Ace") { dHAceCount++; }
                dS += card.cV;
            }

            Console.Clear();
            Console.Write("Bet: $" + bet + "\nYour Hand: ");
            foreach (Card card in pH)
            {
                Console.Write(card.card + " ");
            }
            Console.WriteLine();
            Console.WriteLine("\nDealer Hand: " + dH.First().card);

            if (pH.Count == 2 && pS == 21)
            {
                runGame = false;
                totalEarnings += Convert.ToDouble(bet) * 1.5;
                Console.WriteLine("\nBlackJack\nPayout: $" + Convert.ToDouble(bet) * 1.5);
            }
            if (dH.Count == 2 && dS == 21)
            {
                totalEarnings -= bet;
                
                runGame = false;
                Console.Clear();
                Console.Write("Bet: $" + bet + "\nYour Hand: ");
                foreach (Card card in pH)
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
                Console.WriteLine("\nDealer got BlackJack");
            }

            if (pH.Count == 2 && pH[0].card == pH[1].card)
            {
                string answer = "";
                bool loop = true;

                Console.WriteLine("\nWhat would you like to do: 'Hit' 'Stand' 'Double Down' 'Split'");
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
                            game.stand();
                            loop = false;
                            break;
                        case "double down":
                            game.doubleD(game);
                            loop = false;
                            break;
                        case "split":
                            game.split();
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
                foreach (Card card in pH)
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
                        Console.WriteLine("\nYour Hand Bust\nPress 'Space' To Continue");
                        Console.ReadKey();
                        BlackJack.results();
                        break;
                    }
                }
                else if (pS == 21)
                {
                    runGame = false;
                    Console.WriteLine("\nYour Hand Got 21\nPress 'Space' To Continue");
                    Console.ReadKey();
                    game.stand();
                }
                else
                {
                    game.actions(game);
                }
            }

            Console.Write("\nTotal Earnings: $" + totalEarnings + "\nWould you like to play again: (Yes / No) ");
            if ("yes" == Console.ReadLine().ToLower()) 
            {
                Console.Clear();
                BlackJack.Main(null); 
            }
        }
        
        //Action distrubution
        public void actions(BlackJack game)
        {
            string answer = "";
            bool loop = true;

            if (pH.Count < 3)
            {
                Console.WriteLine("\nWhat would you like to do: 'Hit' 'Stand' 'Double Down'");
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
                            game.stand();
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
            else
            {
                Console.WriteLine("\nWhat would you like to do: 'Hit' 'Stand'");
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
                            game.stand();
                            loop = false;
                            break;
                        default:
                            Console.WriteLine("Invalid Response");
                            break;
                    }
                }
            }
        }

        //Player actions
        public void hit()
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
        public void stand()
        {
            runGame = false;

            while (true)
            {
                Console.Clear();
                Console.Write("Bet: $" + bet + "\nYour Hand: ");
                foreach (Card card in pH)
                {
                    Console.Write(card.card + " ");
                }
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
                    BlackJack.results();
                    break;
                }
                else if (dS > 21)
                {
                    BlackJack.results();
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
        public void doubleD(BlackJack game)
        {
            bet *= 2;

            game.hit();
            game.stand();
        }
        public void split()
        {
            runGame = false;
            Split.Main(null);
        }

        //Single-hand results
        public static void results()
        {
            Console.Clear();

            Console.Write("Bet: $" + bet + "\nYour Hand: ");
            foreach (Card card in pH)
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

            if (pS > 21) { Console.WriteLine("\nYour Hand Bust\nDealer Wins"); totalEarnings -= bet; }
            else if (dS > 21) { Console.WriteLine("\nDealer Bust\nPayout: $" + bet * 2); totalEarnings += (bet * 2); }
            else if (pS == dS) { Console.WriteLine("\nYour was a Stand\nPayout: $" + bet); }
            else if (pS > dS && pS <= 21) { Console.WriteLine("\nYour Hand Won\nPayout: $" + bet * 2); totalEarnings += (bet * 2);  }
            else if (dS > pS && dS <= 21) { Console.WriteLine("\nDealer Beat Your Hand"); totalEarnings -= bet; }
        }

        //Deck creation
        public static void createDeck()
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
