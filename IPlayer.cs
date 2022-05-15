using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConnectFour
{
    internal interface IPlayer
    {
        void MakeMove(Board board, ICollection<IPlayer> opponents);
        string Name { get; }
    }
}
