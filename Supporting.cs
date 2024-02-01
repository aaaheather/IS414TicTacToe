using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class Supporting
    {
        private bool AllEqual(int[] position)
        {
            // Compares all values for equality
            bool allEqual = true;
            for (int i = 0; i < position.Length; i++)
            {
                if (i != 0)
                {
                    if (!(allEqual && position[0].Equals(position[i])))
                    {
                        allEqual = false;
                        break;
                    }
                }
            }
            return allEqual;
        }
        private int Horizontals(int row, int col, int side)
        {
            return row + col * side;
        }
        private int Verticals(int row, int col, int side)
        {
            return col + row * side;
        }
        delegate int Indexer(int row, int col, int side);
        private int RightDiagonals(int position, int side)
        {
            return (position + 1) * (side - 1);
        }
        private int LeftDiagonals(int position, int side)
        {
            return position * side + position;
        }
        delegate int D_Indexer(int position, int side);
        private (bool, int) StateCheck((bool, int) state)
        {
            if (state.Item1)
            {
                // Game winner
                return state;
            }
            // Return Defaults
            return (false, 0);
        }
        private (bool, int) CheckPattern(int[] pattern)
        {
            // Gets the pattern status from defaults or the win logic
            if (AllEqual(pattern))
            {
                // Modified game winner state check
                int winner = pattern[0];
                if (winner != 0)
                {
                    return (true, winner);
                }
            }
            // Return defaults
            return (false, 0);
        }
        private (bool, int) CheckDiagonals(int[] GameBoard)
        {
            // Gets game status from defaults or all diagonal slices on the board if winner
            // Calculating side length of a square gameboard
            int side = (int)Math.Sqrt(GameBoard.Length);
            // Diagonal Indexing Functions for n sized boards
            D_Indexer[] idxs = [RightDiagonals, LeftDiagonals];
            foreach(D_Indexer idx in idxs)
            {
                // Getting pattern of each slice
                List<int> mut_pattern = new List<int>();
                for (int j = 0; j < side;  j++)
                {
                    // Indexing the gameboard with the mapped functions
                    mut_pattern.Add(GameBoard[idx(j, side)]);
                }
                int[] pattern = mut_pattern.ToArray();
                // Game winner state check
                (bool, int) state = StateCheck(CheckPattern(pattern));
                if (state.Item1)
                {
                    return state;
                }
            }
            // Return defaults
            return (false, 0);
        }
        private (bool, int) CheckSlices(int[] GameBoard)
        {
            // Gets game status from defaults or all linear slices on the board if winner
            // Calculating side length of a square gameboard
            int side = (int)Math.Sqrt(GameBoard.Length);
            // Linear Non-Diagonal Indexing Functions for n sized boards
            Indexer[] idxs = [Horizontals, Verticals];
            foreach(Indexer idx in idxs)
            {
                for (int row = 0; row < side; row++)
                {
                    // Getting pattern of each slice
                    List<int> mut_pattern = new List<int>();
                    for (int col = 0; col < side; col++)
                    {
                        // Indexing the gameboard with the mapped functions
                        mut_pattern.Add(GameBoard[idx(row, col, side)]);
                    }
                    int[] pattern = mut_pattern.ToArray();
                    // Game winner state check
                    (bool, int) _state = StateCheck(CheckPattern(pattern));
                    if (_state.Item1)
                    {
                        return _state;
                    }
                }
            }
            // Game winner state check
            (bool, int) state = StateCheck(CheckDiagonals(GameBoard));
            if (state.Item1)
            {
                return state;
            }
            // Return defaults
            return (false, 0);
        }
        private (bool, int) GameStatus(int[] GameBoard)
        {
            // Gets game status from defaults or CheckSlices results if a winner
            // Game winner state check
            (bool, int) state = StateCheck(CheckSlices(GameBoard));
            if (state.Item1)
            {
                return state;
            }
            // Return defaults
            return (false, 0);
        }
        public void PrintBoard(int[] GameBoard)
        {

        }
    }
}
