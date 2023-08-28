using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section12ChessGame.Entities.BoardClasses
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int AmountOfMoviments { get; protected set; }
        public Board Board { get; protected set; }

        public Piece( Color color, Board board)
        {
            Position = null;
            Color = color;
            Board = board;
            AmountOfMoviments = 0;
        }

        public void IncrementAmountOfMoviments()
        {
            AmountOfMoviments++;
        }

        internal void DecrementAmountOfMoviments()
        {
            AmountOfMoviments--;
        }

        public bool ThereArePossibleMoviments()
        {
            bool[,] mat = PossibleMoviments();

            for(int i = 0; i < Board.Rows; i++)
            {
                for(int j = 0; j < Board.Columns; j++ )
                {
                    if (mat[i, j] == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool PossibleMoviment(Position pos)
        {
            return PossibleMoviments()[pos.Row, pos.Column];
        }

        public abstract bool[,] PossibleMoviments();

    }

}

