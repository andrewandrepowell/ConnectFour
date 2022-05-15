using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConnectFour
{
    internal class Human : IPlayer
    {
        private readonly string _name;
        public Human(string name)
        {
            Trace.Assert(name != null);
            _name = name!;
        }
        public string Name 
        {
            get => _name;
        }
        public void MakeMove(Board board, ICollection<IPlayer> opponents)
        {
            Trace.Assert(board != null);
            Trace.Assert(opponents != null);
            Trace.Assert(!board!.IsFull());
            Trace.Assert(!opponents!.Contains(this));

            // Keep trying to get a valid move from player.
            do
            {
                try
                {
                    int col;
                    string? input;
                    Console.WriteLine("Player {0}, please enter the column where you want to place your piece.", _name);
                    Console.WriteLine("Columns must be in range [0, {0}]. Columns are from left to right.", board.Cols - 1);
                    input = Console.ReadLine();
                    if (input == null)
                    {
                        Console.WriteLine("Invalid column.");
                        continue;
                    }
                    col = int.Parse(input);
                    Console.WriteLine("Selected col: {0}", col);
                    if (col < 0 || col >= board.Cols)
                    {
                        Console.WriteLine("Invalid column.");
                        continue;
                    }
                    if (board.IsFull(col))
                    {
                        Console.WriteLine("Column {0} is already full.", col);
                        continue;
                    }
                    board[col] = new Piece(this);
                    Console.WriteLine("Placed piece at column {0}!", col);
                    return;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Incorrect format!");
                }
            }
            while (true);
        }
    }
}
