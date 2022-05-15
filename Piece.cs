using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConnectFour
{
    internal class Piece
    {
        private readonly IPlayer _player;
        public Piece(IPlayer player)
        {
            Trace.Assert(player != null);
            _player = player!;
        }
        public IPlayer Player { get { return _player; } }
    }
}
