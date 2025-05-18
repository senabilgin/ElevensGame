Senanur Bilgin
Computer Science @ BMCC
Class of 2025


# Elevens Card Game (C# Console App)

A console-based version of the classic **Elevens** card game built in C#.

## 🎮 Game Rules

- The board always has **9 cards**.
- You can remove:
  - **Two cards** that add up to **11**  
  - **Three cards**: one **Jack**, one **Queen**, and one **King**
- Each time you remove a valid set, new cards are added from the deck.
- The game ends when:
  - There are **no more valid moves**
  - The deck is **empty** and no matches remain on the board

## 🕹️ How to Play

1. Run the program and choose **Start Game**.
2. The board shows cards labeled 1–9.
3. Enter the numbers of the cards you want to remove. Examples:
   - `2 5` → tries to remove cards 2 and 5 (sum must be 11)
   - `1 4 7` → tries to remove a Jack, Queen, and King
4. Type `q` to quit the game.

## 🧰 Technologies Used

- C#
- .NET 6.0+
- Console Application

## ▶️ How to Run

You can run it using Visual Studio, Visual Studio Code, or command line:

```bash
dotnet run
