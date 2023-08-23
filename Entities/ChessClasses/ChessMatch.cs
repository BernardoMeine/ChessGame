using Section12ChessGame.Entities.BoardClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section12ChessGame.Entities.ChessClasses
{
    internal class ChessMatch
    {
        public Board Board { get; private set; }
        private int Turn;
        private Color CurrentPlayer;
        public bool Finished { get; private set; }
        

        public ChessMatch() {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            PlacePieces();
        }

        public void ExecuteMoviment(Position origin, Position destiny)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncrementAmountOfMoviments();
            Piece CapturedPiece = Board.RemovePiece(destiny);
            Board.PlacePiece(p, destiny);
        }

        private void PlacePieces()
        {
            Board.PlacePiece(new Tower(Color.White, Board), new ChessPosition('c', 1).ConvertToPosition());
            Board.PlacePiece(new King(Color.White, Board), new ChessPosition('d', 1).ConvertToPosition());
            Board.PlacePiece(new Tower(Color.White, Board), new ChessPosition('e', 1).ConvertToPosition());
            Board.PlacePiece(new Tower(Color.White, Board), new ChessPosition('c', 2).ConvertToPosition());
            Board.PlacePiece(new Tower(Color.White, Board), new ChessPosition('d', 2).ConvertToPosition());
            Board.PlacePiece(new Tower(Color.White, Board), new ChessPosition('e', 2).ConvertToPosition());

            Board.PlacePiece(new Tower(Color.Black, Board), new ChessPosition('c', 8).ConvertToPosition());
            Board.PlacePiece(new King(Color.Black, Board), new ChessPosition('d', 8).ConvertToPosition());
            Board.PlacePiece(new Tower(Color.Black, Board), new ChessPosition('e', 8).ConvertToPosition());
            Board.PlacePiece(new Tower(Color.Black, Board), new ChessPosition('c', 7).ConvertToPosition());
            Board.PlacePiece(new Tower(Color.Black, Board), new ChessPosition('d', 7).ConvertToPosition());
            Board.PlacePiece(new Tower(Color.Black, Board), new ChessPosition('e', 7).ConvertToPosition());
        }
    }
}
