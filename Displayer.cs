using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConnectFour
{
    internal class Displayer
    {
        private const char BOARD_SYMBOL = '|';
        private const char EMPTY_SYMBOL = ' ';

        public static void Display(Board board, IDictionary<IPlayer, char> player_symbol_map)
        {
            Trace.Assert(board != null);
            Trace.Assert(player_symbol_map != null);
            Trace.Assert(!player_symbol_map!.Values.Contains(BOARD_SYMBOL), $"BOARD_SYMBOL {BOARD_SYMBOL} can't be a value in player_symbol_map.");

            for (int i = 0; i < ((2 * board!.Cols) + 1); i++)
                Console.Write(BOARD_SYMBOL);
            Console.Write('\n');
            for (int row = 0; row < board!.Rows; row++)
            {
                Console.Write(BOARD_SYMBOL);
                for (int col = 0; col < board!.Cols; col++)
                {
                    Piece? piece = board[row, col];
                    if (piece == null)
                    {
                        Console.Write(EMPTY_SYMBOL);
                    }
                    else
                    {
                        Console.Write(player_symbol_map[piece.Player]);
                    }
                    Console.Write(BOARD_SYMBOL);
                }
                Console.Write('\n');
                for (int i = 0; i < ((2 * board!.Cols) + 1); i++)
                    Console.Write(BOARD_SYMBOL);
                Console.Write('\n');
            }
        }
    }
}
