using System;
using System.Collections.Generic;
namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //We need a way to create a 52 card deck:
            var suits = new List<string>() { "Clubs", "Diamonds", "Hearts", "Spades" };
            var ranks = new List<string>() { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };

            var deck = new List<string>();
            //We Need a loop to match each suit to each one of the ranks.
            foreach (var suit in suits)
            {
                foreach (var rank in ranks)
                {
                    var card = $"{rank} of {suit}";

                    //We need to add the newly formed string called card to the end of the deck:
                    deck.Add(card);
                }
            }

            //Define what numberOfCards is:
            var numberOfCards = deck.Count;

            //????????????????
            for (var rightIndex = numberOfCards - 1; rightIndex >= 1; rightIndex--)
            {
                //leftIndex = random integer that is greater than or equal to 0 and LESS than rightIndex.
                var randomNumberGenerator = new Random();
                var leftIndex = randomNumberGenerator.Next(rightIndex);

                //Now swap the values at rightIndex and leftIndex by doing this:

                //leftCard = the value from deck[leftIndex]
                var leftCard = deck[leftIndex];
                // rightCard= the value from deck[rightIndex]
                var rightCard = deck[rightIndex];

                deck[rightIndex] = leftCard;
                deck[leftIndex] = rightCard;
            }

            //exited shuffle loop notification
            Console.WriteLine("Done shuffling");
            ////////////////////////////////////////////////////

            //firstCard define it
            var firstCard = deck[0];
            //secondCard define it
            var secondCard = deck[1];

            // Print on screen the first and second card of the shuffled deck
            Console.WriteLine($"The first card is {firstCard} and the second card is {secondCard}");








        }
    }
}