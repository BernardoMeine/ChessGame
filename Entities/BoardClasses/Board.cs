using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section12ChessGame.Entities.BoardClasses
{
    internal class Board
    {
        public int  Rows { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces { get; set; }

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Pieces = new Piece[Rows, Columns];
        }

        public Piece Piece (int row, int column) 
        { 
            return Pieces[row, column];
        }

        public void PlacePiece (Piece p, Position pos)
        {
            Pieces[pos.Row, pos.Column] = p;

            p.Position = pos;
        } 
    }
}
