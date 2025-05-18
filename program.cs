using System;
using System.Collections.Generic;

// Enums for Suit and Rank
public enum Suit { Hearts, Diamonds, Clubs, Spades }
public enum Rank { Ace = 1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King }

// Card Class
public class Card
{
    public Suit Suit { get; set; }
    public Rank Rank { get; set; }
    public int Value => (int)Rank;

    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }

    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }
}

// Deck Class
public class Deck
{
    public List<Card> Cards { get; private set; } = new List<Card>();

    public void CreateDeck()
    {
        Cards.Clear();
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                Cards.Add(new Card(suit, rank));
            }
        }
    }

    public void Shuffle()
    {
        Random rng = new Random();
        for (int i = Cards.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (Cards[i], Cards[j]) = (Cards[j], Cards[i]);
        }
    }

    public Card DrawCard()
    {
        if (Cards.Count == 0) return null;
        Card drawn = Cards[0];
        Cards.RemoveAt(0);
        return drawn;
    }
}

// Game Class
public class Game
{
    private Deck Deck = new Deck();
    private List<Card> Board = new List<Card>();

    public void StartGame()
    {
        Deck.CreateDeck();
        Deck.Shuffle();
        DealInitialBoard();
        PlayGameLoop();
    }

    private void DealInitialBoard()
    {
        Board.Clear();
        for (int i = 0; i < 9 && Deck.Cards.Count > 0; i++)
        {
            Board.Add(Deck.DrawCard());
        }
    }

    private void DisplayBoard()
    {
        Console.WriteLine("\nCurrent Board:");
        for (int i = 0; i < Board.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Board[i]} (Value: {Board[i].Value})");
        }
    }

    private void PlayGameLoop()
    {
        while (true)
        {
            DisplayBoard();

            if (!HasPossibleMoves())
            {
                Console.WriteLine("No more possible moves. Game over!");
                return;
            }

            Console.WriteLine("Select cards to remove (e.g. '1 2' or '3 5 7' for JQK), or 'q' to quit:");
            string input = Console.ReadLine().ToLower();
            if (input == "q") return;

            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            List<int> indices = new List<int>();

            foreach (string part in parts)
            {
                if (int.TryParse(part, out int index) && index >= 1 && index <= Board.Count)
                    indices.Add(index - 1);
            }

            if (IsValidMove(indices))
            {
                RemoveCards(indices);
                RefillBoard();
            }
            else
            {
                Console.WriteLine("Invalid move. Try again.");
            }
        }
    }

    private bool IsValidMove(List<int> indices)
    {
        if (indices.Count == 2)
        {
            int sum = Board[indices[0]].Value + Board[indices[1]].Value;
            return sum == 11;
        }
        else if (indices.Count == 3)
        {
            bool hasJ = false, hasQ = false, hasK = false;
            foreach (int i in indices)
            {
                if (Board[i].Rank == Rank.Jack) hasJ = true;
                if (Board[i].Rank == Rank.Queen) hasQ = true;
                if (Board[i].Rank == Rank.King) hasK = true;
            }
            return hasJ && hasQ && hasK;
        }
        return false;
    }

    private void RemoveCards(List<int> indices)
    {
        indices.Sort((a, b) => b.CompareTo(a)); // remove highest index first
        foreach (int i in indices)
        {
            Board.RemoveAt(i);
        }
    }

    private void RefillBoard()
    {
        while (Board.Count < 9 && Deck.Cards.Count > 0)
        {
            Board.Add(Deck.DrawCard());
        }
    }

    private bool HasPossibleMoves()
    {
        for (int i = 0; i < Board.Count; i++)
        {
            for (int j = i + 1; j < Board.Count; j++)
            {
                if (Board[i].Value + Board[j].Value == 11)
                    return true;
            }
        }

        bool hasJ = false, hasQ = false, hasK = false;
        foreach (Card card in Board)
        {
            if (card.Rank == Rank.Jack) hasJ = true;
            if (card.Rank == Rank.Queen) hasQ = true;
            if (card.Rank == Rank.King) hasK = true;
        }

        return hasJ && hasQ && hasK;
    }
}

// Program Entry Point
class Program
{
    static void Main()
    {
        Game game = new Game();
        bool playing = true;

        while (playing)
        {
            Console.WriteLine("\n=== Elevens Game ===");
            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                game.StartGame();

                Console.WriteLine("\nDo you want to play again? (y/n)");
                string response = Console.ReadLine().ToLower();
                if (response != "y") playing = false;
            }
            else if (choice == "2")
            {
                playing = false;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }

        Console.WriteLine("Thanks for playing!");
    }
}
