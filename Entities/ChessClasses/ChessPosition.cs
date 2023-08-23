using Section12ChessGame.Entities.BoardClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section12ChessGame.Entities.ChessClasses
{
    internal class ChessPosition
    {
        public char Row { get; set; }
        public int Column { get; set; }

        public ChessPosition(char row, int column)
        {
            Row = row;
            Column = column;
        }

        public Position ConvertToPosition()
        {
            return new Position(8 - Row, Column - 'a');
        }

        public override string ToString()
        {
            return $"{Column}{Row}";
        }
    }
}
