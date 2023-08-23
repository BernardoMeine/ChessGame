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

        public Piece Piece (Position pos)
        {
            return Pieces[pos.Row, pos.Column];
        }

        public void PlacePiece (Piece p, Position pos)
        {
            if(PieceExists(pos))
            {
                throw new BoardException("There is already a piece on that position!");
            }
            Pieces[pos.Row, pos.Column] = p;

            p.Position = pos;
        }

        public bool PieceExists (Position pos)
        {
            ValidatePosition(pos);
            return Piece(pos) != null;
        }
        
        public bool ValidPosition (Position pos)
        {
            if(pos.Row < 0 || pos.Row >= Rows || pos.Column < 0 || pos.Column >= Columns)
            {
                return false;
            }

            return true;
        }

        public void ValidatePosition (Position pos)
        {
            if(!ValidPosition(pos))
            {
                throw new BoardException("Invalid position!");
            }
        }
    }
}
