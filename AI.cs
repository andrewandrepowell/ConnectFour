using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConnectFour
{
    internal class AI : IPlayer
    {
        private readonly string _name;
        private readonly int _rand_chance;
        private readonly static Random _random = new Random();
        public AI(int id, int rand_chance = 20)
        {
            Trace.Assert(id >= 0);
            Trace.Assert(rand_chance >= 0 && rand_chance <= 100);
            _name = String.Format("BEEP-BOOP-{0}", id);
            _rand_chance = rand_chance;
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
            
            int i;

            // Create a strategy for the AI, and determine moves.
#if DEBUG
            Console.WriteLine($"{this.Name}'s Strategy: ");
#endif
            Strategy self_stragey = new Strategy(this, board);
            int self_best_move, self_best_weight, self_random_move;
            self_stragey.GetBestMove(out self_best_move, out self_best_weight);
            self_random_move = self_stragey.GetRandomMove();

            // Determine best move for the opponents.
            int opp_best_move, opp_best_weight;
            int[] opp_moves = new int[opponents.Count];
            int[] opp_weights = new int[opponents.Count];
            i = 0;
            foreach(IPlayer opponent in opponents)
            {
#if DEBUG
                Console.WriteLine($"{opponent.Name}'s Strategy: ");
#endif
                new Strategy(opponent, board).GetBestMove(out opp_moves[i], out opp_weights[i]);
                i++;
            }
            Math.Max(opp_weights, out opp_best_weight, out i); // i is being reused here.
            opp_best_move = opp_moves[i];

            // Determine chance to go random.
            bool go_random = _random.Next(0, 99) < _rand_chance;

            // Random choice.
            if (go_random)
            {
                board[self_random_move] = new Piece(this);
            }
            // If AI can win, go ahead and win.
            else if (self_best_weight >= board.WinC)
            {
                board[self_best_move] = new Piece(this);
            }
            // If opponent can win, go ahead and block them.
            else if (opp_best_weight >= board.WinC)
            {
                board[opp_best_move] = new Piece(this);
            }
            // If the weight is 1--really shouldn't be zero--then AI hasn't played yet. Go random.
            else if (self_best_weight <= 1)
            {
                board[self_random_move] = new Piece(this);
            }
            // For any other case, just go best move.
            else
            {
                board[self_best_move] = new Piece(this);
            }
        }
    }
}
