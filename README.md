# Blackjack

//In this assignment I watched a reference video created by Youtube user name cornerserenader in 2010.
Link is https://www.youtube.com/watch?v=xfNzUaOOY6w&list=PL-OugP_AfIPGxpTUi2otXwvVTut0b5Ui6&index=1

Objective:
We want to create a one player game of Blackjack. Where the user plays against the computer. Effectively use loops, conditionals, and other control structures to implement control flows. Demonstrate usage of data structures to model resources.

Requirements:
General Rules:

    The game should be played with a standard deck of playing cards (52).

    The house should be dealt two cards, hidden from the player until the house reveals its hand.

    The player should be dealt two cards, visible to the player.

    The player should have a chance to hit (i.e. be dealt another card) until they decide to stop or they bust (i.e. their total is over 21). At which point they lose regardless of the dealer's hand.

    When the player stands, the house reveals its hand and hits (i.e. draw cards) until they have 17 or more.

    If dealer goes over 21 the dealer loses.

    The player should have two choices: "Hit" and "Stand."

    Consider Aces to be worth 11, never 1.

    The app should display the winner. For this mode, the winner is who is closer to a blackjack (21) without going over.

    Ties go to the DEALER

    There should be an option to play again. This should start a new game with a new full deck of 52 shuffled cards and new empty hands for the dealer and the player.

/////////////EXPLORER MODE ASSIGNMENT:
We will work out the assignment up to the algorithm part of PEDAC method.

//"P"roblem: redescribe.
We want to create a simple user interface that will act as an interface with the computer. At the start of the program we will display a greeting.
There needs to be class representing a standard 52 card deck. This deck needs to be shuffled at the start of each game.
The player has only two choices in the game: to "Hit" or to "Stand".
Aces in our game will be worth 11, never 1.
We want to display who won the game at the end.
Ties go to the DEALER in our game.
At the end have the option to play again, restarting everything shuffled deck and new dealt hands.

//"E"xamples: do at least 6 of these types.

1. For example, if the user starts with AceofDiamonds and 9ofSpades. User decides to "Stand" on their first turn. What happens?
   First we reveal the dealer hand to the user. In this case lets assume the dealer had 5ofDiamonds and 10ofHearts. We display this ont the screen.
   This means the dealer has 15 and the user has 20. The dealer has to Hit until his number is at least at 17.
   Dealer "Hits" once. He gets 8ofSpades. This puts dealer at 23 which is a BUST.
   We display the result: Dealer Bust! You Win!
   Games asks user to play again yes or no. If yes restart everything for a new game to start. If no then quit program.

2. User gets 2ofHearts and JofSpades (12 total). Decides to Hit. Gets 9ofClubs (21).WHAT HAPPENS HERE?

User got to Blackjack (21) after the first "Hit". Immediately display to the user the "dealer hand" to be "2ofClubs and 6ofDiamonds" (8).
At this point we need to switch over to only having the dealer draw cards and NOT let user draw/play more cards.
8 is > 17, so...
dealer "Hit" again. The third card is 8ofSpades (16).

dealer hand is 2clubs, 6diamonds, 8spades for a total of 16, dealer Hits again.

The fourth card is 9ofHearts. That puts dealer at (25).

Game over. Display "Dealer Bust! You Win!".

Ask user to play again y/n.
if yes, restart a new game.
if no, then end program.

3.  User gets 4ofClubs and 4ofHearts (8). They Hit. Gets 6ofSpades (14). Hits again and gets JofHearts (24). WHAT happens?

    This user busted on the 4th card with 24.
    Immediately show what the dealer hand is: "QofSpades and 9ofSpades" (19).
    Dealer Stands.
    Display "Dealer wins! You Bust.".
    Ask user to play again y/n.

4.  User gets KofSpades and 2ofDiamonds (12). They Hit. Gets 10ofDiamonds (22). What happens next?

    Immediately display to user "Bust!".
    then,
    Display the dealer hand.
    Dealer has 4ofHearts and 2ofSpades (6).

    Display "Dealer Wins!"
    Ask user to play again y/n.

5.  User gets QofDiamonds and 7ofDiamonds (17). They Stand. What happens?

    Immediately display the dealer hand. It is 5ofClubs and 9ofDiamonds (14).
    Dealer Hits. Gets 5ofHearts (19).

    Display dealer wins.
    Ask user to play again y/n.

6.  User gets 3ofClubs and 5ofDiamonds (8). They Hit. Gets AofHearts (19). They Stand. What happens?

    Immediately display the dealer hand of 8ofClubs and 2ofSpades (10).
    Dealer Hit. Gets AofDiamonds (21).
    Game over, display "Dealer wins!"
