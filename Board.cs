using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConnectFour
{
    internal class Board
    {
        private readonly int _rows;
        private readonly int _cols;
        private readonly int _winc;
        private readonly Piece?[,] _board;

        public Board(int rows, int cols, int winc)
        {
            Trace.Assert(rows > 0);
            Trace.Assert(cols > 0);
            Trace.Assert(winc > 0 && winc <= Math.Min(new int[] { rows, cols }));
            _board = new Piece?[rows, cols];
            _rows = rows;
            _cols = cols;
            _winc = winc;
        }

        public int Cols { get { return _cols; } }
        public int Rows { get { return _rows; } }
        public int WinC { get { return _winc; } }

        public Piece? this[int row, int col] 
        { 
            get => _board[row, col];  
        }

        public Piece? this[int col]
        {
            set 
            {
                Trace.Assert(col >= 0 && col < _cols);
                Trace.Assert(!IsFull(col));
                Trace.Assert(_board[0, col] == null);
                Trace.Assert(value != null);

                // Determine next available spot in column.
                int row;
                for (row = 0; row < _rows; row++)
                {
                    if ((row + 1) >= _rows || _board[row + 1, col] != null)
                        break;
                }
                Trace.Assert(row < _rows);

                // Set the board with the new piece.
                _board[row, col] = value;
            }
        }

        public IPlayer? GetWinner()
        {
            // Check rows for winner.
            for (int row = 0; row < _rows; row++)
            {
                IPlayer? player = null;
                int amount = 0;
                for (int col = 0; col < _cols; col++)
                {
                    Piece? piece = _board[row, col];
                    if (piece == null)
                    {
                        player = null;
                    }
                    else if (player == null || piece.Player != player)
                    {

                        player = piece.Player;
                        amount = 0;
                    }
                    else if (piece.Player == player)
                    {
                        amount++;
                    }
                    if (player != null && amount == (_winc - 1))
                    {
                        return player;
                    }
                }
            }

            // Check columns for winner.
            for (int col = 0; col < _cols; col++)
            {
                IPlayer? player = null;
                int amount = 0;
                for (int row = 0; row < _rows; row++)
                {
                    Piece? piece = _board[row, col];
                    if (piece == null)
                    {
                        player = null;
                    }
                    else if (player == null || piece.Player != player)
                    {
                        player = piece.Player;
                        amount = 0;
                    }
                    else if (piece.Player == player)
                    {
                        amount++;
                    }
                    if (player != null && amount == (_winc - 1))
                    {
                        return player;
                    }
                }
            }

            // Check diagonals for winner.
            for (int dag = 0; dag < _cols; dag++)
            {
                IPlayer? player;
                int amount, col, row;

                player = null;
                amount = 0;
                row = 0;
                col = dag;
                while (row != _rows && col != _cols)
                {
                    Piece? piece = _board[row, col];
                    if (piece == null)
                    {
                        player = null;
                    }
                    else if (player == null || piece.Player != player)
                    {
                        player = piece.Player;
                        amount = 0;
                    }
                    else if (piece.Player == player)
                    {
                        amount++;
                    }
                    if (player != null && amount == (_winc - 1))
                    {
                        return player;
                    }

                    row++;
                    col++;
                }

                player = null;
                amount = 0;
                row = 0;
                col = dag;
                while (row != _rows && col >= 0)
                {
                    Piece? piece = _board[row, col];
                    if (piece == null)
                    {
                        player = null;
                    }
                    else if (player == null || piece.Player != player)
                    {
                        player = piece.Player;
                        amount = 0;
                    }
                    else if (piece.Player == player)
                    {
                        amount++;
                    }
                    if (player != null && amount == (_winc - 1))
                    {
                        return player;
                    }

                    row++;
                    col--;
                }
            }

            // Check diagonals for winner.
            for (int dag = 0; dag < _rows; dag++)
            {
                IPlayer? player;
                int amount, col, row;

                player = null;
                amount = 0;
                row = dag;
                col = 0;
                while (row != _rows && col != _cols)
                {
                    Piece? piece = _board[row, col];
                    if (piece == null)
                    {
                        player = null;
                    }
                    else if (player == null || piece.Player != player)
                    {
                        player = piece.Player;
                        amount = 0;
                    }
                    else if (piece.Player == player)
                    {
                        amount++;
                    }
                    if (player != null && amount == (_winc - 1))
                    {
                        return player;
                    }

                    row++;
                    col++;
                }

                player = null;
                amount = 0;
                row = dag;
                col = _cols - 1;
                while (row != _rows && col >= 0)
                {
                    Piece? piece = _board[row, col];
                    if (piece == null)
                    {
                        player = null;
                    }
                    else if (player == null || piece.Player != player)
                    {
                        player = piece.Player;
                        amount = 0;
                    }
                    else if (piece.Player == player)
                    {
                        amount++;
                    }
                    if (player != null && amount == (_winc - 1))
                    {
                        return player;
                    }

                    row++;
                    col--;
                }
            }
            return null;
        }
        public bool IsFull(int col)
        {
            Trace.Assert(col >= 0 && col < _cols);

            for (int row = 0; row < _rows; row++)
            {
                if (_board[row, col] == null)
                    return false;
            }
            return true;
        }

        public bool IsFull()
        {
            for (int row = 0; row < _rows; row++)
            {
                for (int col = 0; col < _cols; col++)
                {
                    if (_board[row, col] == null)
                        return false;
                }
            }
            return true;
        }

    }
}
