using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConnectFour
{
    internal class Strategy
    {
        private readonly IPlayer _player;
        private readonly Board _board;
        public Strategy(IPlayer player, Board board)
        {
            Trace.Assert(player != null);
            Trace.Assert(board != null);
            _player = player!;
            _board = board!;
        }

        public void GetBestMove(out int best_col, out int best_weight)
        {
            Trace.Assert(!_board.IsFull());

            int[] weights = new int[_board.Cols];

            // Perform operation on each column. 
            for (int col = 0; col < _board.Cols; col++)
            {
                // If the column is full, no reason to determine weight, so continue to next column.
                if (_board.IsFull(col))
                    continue;

                Trace.Assert(_board[0, col] == null);

                // Determine the available space.
                int avail_row = 0;
                int avail_col = col;
                for (avail_row = 0; (avail_row + 1) < _board.Rows && _board[avail_row + 1, col] == null; avail_row++)
                {
                }

                // Collect the weights from each dimension.
                List<int> window_weights = new List<int>();

                // Check for possible vertical win.
                {
                    int weight = 0;
                    for (int index = 0;
                        (index < _board.WinC) &&
                        ((index + avail_row + 1) < _board.Rows);
                        index++)
                    {
                        Piece? piece = _board[index + avail_row + 1, avail_col];
                        Trace.Assert(piece != null);
                        if (piece!.Player == _player)
                        {
                            weight++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    window_weights.Add(weight);
                }

                // Employ horizontal sliding window.
                for (int window = 0; window < _board.WinC; window++)
                {
                    // If the window is out of bounds, then skip.
                    if (!(avail_col - (_board.WinC - window - 1) >= 0) ||
                        !((avail_col + window) < _board.Cols))
                    {
                        continue;
                    }
                    // Compute wieght of window.
                    int weight = 0;
                    for (int index = 0; index < _board.WinC; index++)
                    {
                        Piece? piece = _board[avail_row, avail_col - (_board.WinC - window - 1) + index];
                        if (piece == null)
                            continue;
                        if (piece.Player == _player)
                        {
                            weight++;
                        }
                        else
                        {
                            weight = 0;
                            break;
                        }
                    }
                    window_weights.Add(weight);
                }

                // Employ diagonal sliding window from bottom-left to upper-right.
                for (int window = 0; window < _board.WinC; window++)
                {
                    // If the window is out of bounds, then skip.
                    if (!(avail_col - (_board.WinC - window - 1) >= 0) ||
                        !((avail_col + window) < _board.Cols) ||
                        !(avail_row + (_board.WinC - window - 1) < _board.Rows) ||
                        !((avail_row - window) >= 0))
                    {
                        continue;
                    }
                    // Compute wieght of window.
                    int weight = 0;
                    for (int index = 0; index < _board.WinC; index++)
                    {
                        Piece? piece = _board[avail_row + (_board.WinC - window - 1) - index, avail_col - (_board.WinC - window - 1) + index];
                        if (piece == null)
                            continue;
                        if (piece.Player == _player)
                        {
                            weight++;
                        }
                        else
                        {
                            weight = 0;
                            break;
                        }
                    }
                    window_weights.Add(weight);
                }

                // Employ diagonal sliding window from bottom-right to upper-left.
                for (int window = 0; window < _board.WinC; window++)
                {
                    // If the window is out of bounds, then skip.
                    if (!(avail_col + (_board.WinC - window - 1) < _board.Cols) ||
                        !((avail_col - window) >= 0) ||
                        !(avail_row + (_board.WinC - window - 1) < _board.Rows) ||
                        !((avail_row - window) >= 0))
                    {
                        continue;
                    }
                    // Compute wieght of window.
                    int weight = 0;
                    for (int index = 0; index < _board.WinC; index++)
                    {
                        Piece? piece = _board[avail_row + (_board.WinC - window - 1) - index, avail_col + (_board.WinC - window - 1) - index];
                        if (piece == null)
                            continue;
                        if (piece.Player == _player)
                        {
                            weight++;
                        }
                        else
                        {
                            weight = 0;
                            break;
                        }
                    }
                    window_weights.Add(weight);
                }

                // The +1 indicates the column at the very least is available.
                weights[avail_col] = Math.Max(window_weights) + 1;
            }

            // The column corresponding to the largest weight is the best move.
            Math.Max(weights, out best_weight, out best_col);

#if DEBUG
            Console.Write("Strategy Weights: ");
            foreach (int weight in weights)
                Console.Write($"{weight} ");
            Console.Write("\n");
#endif

            Trace.Assert(best_weight >= 0);
            Trace.Assert(best_col >= 0 && best_col < _board.Cols);
        }

        public int GetRandomMove()
        {
            Trace.Assert(!_board.IsFull());
            
            // Determine available columns.
            List<int> avail_cols = new List<int>();
            for (int col = 0; col < _board.Cols; col++)
                if (!_board.IsFull(col))
                    avail_cols.Add(col);

            // Randomly pick from the list.
            int rand_move = avail_cols[new Random().Next(0, avail_cols.Count - 1)];

            Trace.Assert(rand_move >= 0 && rand_move < _board.Cols);
            return rand_move;
        }
    }
}
