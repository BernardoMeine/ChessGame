using Section12ChessGame.Entities.BoardClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section12ChessGame.Entities.ChessClasses
{
    internal class Tower : Piece
    {
        public Tower(Color color, Board board)
            : base(color, board)
        {
        }

        public override string ToString()
        {
            return "T";
        }
    }
}
