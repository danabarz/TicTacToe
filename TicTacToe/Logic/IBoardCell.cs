using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Logic
{
    public interface IBoardCell
    {
        int Row { get; }
        int Column { get; }
        PlayerMarker? OwningPlayer { get; }
    }
}
