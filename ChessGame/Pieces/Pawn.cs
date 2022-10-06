using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Pieces
{
    internal class Pawn : Piece
    {
        public Pawn(int team, string name="p") : base(team, name)
        {
        }
    }
}
