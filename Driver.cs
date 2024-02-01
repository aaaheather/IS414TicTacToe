using System;

namespace TicTacToe
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Tic-Tac-Toe!");

            // Create an instance of the supporting class
            Supporting supporting = new Supporting();

            // Create a game board array to store the players' choices
            int[] gameBoard = new int[9]; // Assuming a 3x3 board

            // Ask each player in turn for their choice and update the game board array
            for (int turn = 1; turn <= 9; turn++)
            {
                Console.WriteLine($"Player {turn % 2 + 1}, enter your choice (1-9): ");
                int choice = int.Parse(Console.ReadLine());

                // Validate the choice and update the game board
                if (choice >= 1 && choice <= 9 && gameBoard[choice - 1] == 0)
                {
                    gameBoard[choice - 1] = turn % 2 + 1;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Try again.");
                    turn--; // Repeat the same turn
                }

                // Print the board
                supporting.PrintBoard(gameBoard);

                // Check for a winner
                (bool isWinner, int winner) = supporting.GameStatus(gameBoard);
                if (isWinner)
                {
                    Console.WriteLine($"Player {winner} wins!");
                    break;
                }

                // If all turns are completed and no winner, it's a draw
                if (turn == 9)
                {
                    Console.WriteLine("It's a draw!");
                }
            }
        }
    }
}
