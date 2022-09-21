using System.Linq;
using System;
using System.Collections.Generic;
namespace BlackjackAssignment
{
    class MainCode
    {
        //create a game state 
        public enum GameResult { Win = 1, Lose = -1, Draw = 0, Pending = 2 };


        //Define what a Card is....
        public class Card
        {
            public string ID { get; set; }
            public string Suit { get; set; }
            public int Value { get; set; }

            public Card(string id, string suit, int value)
            {
                ID = id;
                Suit = suit;
                Value = value;
            }
        }

        //The Deck is represented as a stack of cards!

        public class Deck : Stack<Card>
        {
            public Deck(IEnumerable<Card> collection) : base(collection) { }
            public Deck() : base(52) { }

            //Implement an index of reference
            public Card this[int index]
            {
                get
                {
                    Card item;

                    if (index >= 0 && index <= this.Count - 1)
                    {
                        item = this.ToArray()[index];
                    }
                    else
                    {
                        item = null;
                    }

                    return item;
                }
            }

            //Define the value of the Deck
            public double Value
            {
                get
                {
                    //Return value
                    return BlackJackRules.HandValue(this);
                }
            }
        }

        //Next define what represents players (Dealer and Player), called Members here.
        public class Member
        {
            public Deck Hand;

            public Member()
            {
                Hand = new Deck();
            }
        }

        //Now lets define an entity for the general game rules///////////////////

        public static class BlackJackRules
        {
            //Start with the card elements.
            public static string[] ids = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

            //Now the suits.
            public static string[] suits = { "C", "D", "H", "S" };

            //Now lets return a new deck
            public static Deck NewDeck
            {
                get
                {
                    Deck d = new Deck();
                    int value;

                    foreach (string suit in suits)
                    {
                        foreach (string id in ids)
                        {
                            value = Int32.TryParse(id, out value) ? value : id == "A" ? 1 : 10;
                            d.Push(new Card(id, suit, value));
                        }
                    }

                    return d;
                }
            }

            //Shuffle the deck!
            public static Deck ShuffledDeck
            {
                get
                {
                    return new Deck(NewDeck.OrderBy(Card => System.Guid.NewGuid()).ToArray());
                }
            }

            //We have to calculate the VALUE of a Hand.
            //This is represented by cards in a Deck<Card> .
            //We compare two totals for aces and return the one closest to "<=21".
            public static double HandValue(Deck deck)
            {
                //Ace equal to 1
                int val1 = deck.Sum(c => c.Value);

                //Ace is equal to 11
                double aces = deck.Count(c => c.Suit == "A");
                //enums!
                double val2 = aces > 0 ? val1 + (10 * aces) : val1;

                return new double[] { val1, val2 }
                    .Select(handVal => new { handVal, weight = Math.Abs(handVal - 21) + (handVal > 21 ? 100 : 0) })

                .OrderBy(n => n.weight)
                .First().handVal;
            }

            //Express rules more:

            //Lets limit the dealer hits. Assuming Dealer will always stand on 17. We will set standLimit.
            public static bool CanDealerHit(Deck deck, int standLimit)
            {
                return deck.Value < standLimit;
            }


            //
            public static bool CanPlayerHit(Deck deck)
            {
                return deck.Value < 21;
            }


            ///<Summary 2.0>//
            //RETURN game state win, lose, or draw given players' hand


            //lets tie the GameResult back in from the beginning of the program. And define win/lose
            public static GameResult GetResult(Member player, Member dealer)
            {
                GameResult res = GameResult.Win; // why notttttt?

                double playerValue = HandValue(player.Hand);
                double dealerValue = HandValue(dealer.Hand);

                //player can win if...
                if (playerValue <= 21)
                {
                    //and...
                    if (playerValue != dealerValue)
                    {
                        double closestValue = new double[] { playerValue, dealerValue }
                            .Select(handVal => new
                            {
                                handVal,
                                weight = Math.Abs(handVal - 21) + (handVal > 21 ? 100 : 0)
                            })
                                .OrderBy(n => n.weight)
                                .First().handVal;

                        res = playerValue == closestValue ? GameResult.Win : GameResult.Lose;
                    }
                    else
                    {
                        res = GameResult.Draw;
                    }
                }

                else
                {
                    res = GameResult.Lose;
                }

                return res;
            }


        } //this is blackjackRules end bracket.

        public class BlackJack
        {
            //Setup game!///////////////////////////////////////////////////////

            public Member Dealer = new Member();
            public Member Player = new Member();
            public GameResult Result { get; set; }
            public Deck MainDeck;

            public int StandLimit { get; set; }

            public BlackJack(int dealerStandLimit)
            {
                //we need to make a way to SETUP a new blackjack game
                Result = GameResult.Pending;

                StandLimit = dealerStandLimit;

                //shuffle deck 
                MainDeck = BlackJackRules.ShuffledDeck;

                //clear Player and Dealer hands
                Dealer.Hand.Clear();
                Player.Hand.Clear();

                //DEAL the first two cards to Player and Dealer
                for (int i = 0; ++i < 3;)
                {
                    Dealer.Hand.Push(MainDeck.Pop());
                    Player.Hand.Push(MainDeck.Pop());
                }
            }


            //Allow Player to hit. Dealer will automaticall Hits when user STANDS!
            public void Hit()
            {
                if (BlackJackRules.CanPlayerHit(Player.Hand) && Result == GameResult.Pending)
                {
                    Player.Hand.Push(MainDeck.Pop());
                }
            }

            //<Summary>
            //When user stands, allow the Dealer to continue hitting until standLimit or bust.

            //Then go ahead and set the game result.
            public void Stand()
            {
                if (Result == GameResult.Pending)
                {
                    while (BlackJackRules.CanDealerHit(Dealer.Hand, StandLimit))
                    {
                        Dealer.Hand.Push(MainDeck.Pop());
                    }

                    Result = BlackJackRules.GetResult(Player, Dealer);
                }
            }
        }

        public class Program
        {
            public static void ShowStats(BlackJack bj)
            {
                //state info
                Console.WriteLine("Dealer");
                foreach (Card c in bj.Dealer.Hand)
                {
                    Console.WriteLine(string.Format("{0}{1}", c.ID, c.Suit));
                }

                Console.WriteLine(bj.Dealer.Hand.Value);

                Console.WriteLine(Environment.NewLine);

                Console.WriteLine("Player");

                foreach (Card c in bj.Player.Hand)
                {
                    Console.WriteLine(string.Format("{0}{1}", c.ID, c.Suit));
                }

                Console.WriteLine(bj.Player.Hand.Value);
                Console.WriteLine(Environment.NewLine);
            }

            public static void Main()
            {
                string input = "";

                BlackJack bj = new BlackJack(17);

                ShowStats(bj);

                while (bj.Result == GameResult.Pending)
                {
                    input = Console.ReadLine();

                    if (input.ToLower() == "h")
                    {
                        bj.Hit();
                        ShowStats(bj);
                    }
                    else
                    {
                        bj.Stand();
                        ShowStats(bj);
                    }
                }

                Console.WriteLine(bj.Result);
                Console.ReadLine();
            }
        }



    } //this is the class program end bracket..
} //this is the namespace BlackjackAssignment end bracket...