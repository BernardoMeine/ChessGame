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

        public abstract bool[,] PossibleMoviments();
    }
}
