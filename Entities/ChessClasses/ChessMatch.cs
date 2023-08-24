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
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }
        public HashSet<Piece> Pieces { get; private set; }
        public  HashSet<Piece> Captured { get; private set; }


        public ChessMatch() {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            PlacePieces();
        }

        public void ExecuteMoviment(Position origin, Position destiny)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncrementAmountOfMoviments();
            Piece CapturedPiece = Board.RemovePiece(destiny);
            Board.PlacePiece(p, destiny);
            if (CapturedPiece != null)
            {
                Captured.Add(CapturedPiece);
            }
        }



        public void PerformMove(Position origin, Position destiny)
        {
            ExecuteMoviment(origin, destiny);
            Turn++;
            ChangePlayer();
        }

        public void ValidateOriginPosition (Position pos)
        {
            if(Board.Piece(pos) == null)
            {
                throw new BoardException("Não existe peça na posição de origem escolhida!");
            }

            if(CurrentPlayer != Board.Piece(pos).Color) 
            {
                throw new BoardException("A peça de origem escolhida não é sua!");
            }

            if (!Board.Piece(pos).ThereArePossibleMoviments())
            {
                throw new BoardException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void ValidateDestinyPosition (Position origin, Position destiny)
        {
            if (!Board.Piece(origin).CanMoveTo(destiny)) {
                throw new BoardException("Posição de destino inválida!");
            }
        }

        public void ChangePlayer()
        {
            if(CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            } else
            {
                CurrentPlayer = Color.White;
            }
        }

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in Captured) { 
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> PiecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in Pieces)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }

        public void PlaceNewPiece(char Column, int Row, Piece piece)
        {
            Board.PlacePiece(piece, new ChessPosition(Column, Row).ConvertToPosition());
            Pieces.Add(piece);
        }

        private void PlacePieces()
        {
            PlaceNewPiece('c', 1, new Tower(Color.White, Board));
            PlaceNewPiece('d', 1, new King(Color.White, Board));
            PlaceNewPiece('e', 1, new Tower(Color.White, Board));
            PlaceNewPiece('c', 2, new Tower(Color.White, Board));
            PlaceNewPiece('d', 2, new Tower(Color.White, Board));
            PlaceNewPiece('e', 2, new Tower(Color.White, Board));

            PlaceNewPiece('c', 8, new Tower(Color.Black, Board));
            PlaceNewPiece('d', 8, new King(Color.Black, Board));
            PlaceNewPiece('e', 8, new Tower(Color.Black, Board));
            PlaceNewPiece('c', 7, new Tower(Color.Black, Board));
            PlaceNewPiece('d', 7, new Tower(Color.Black, Board));
            PlaceNewPiece('e', 7, new Tower(Color.Black, Board));
        }

        
    }
}
