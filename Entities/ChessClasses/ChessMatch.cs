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
        public bool Check { get; private set; }


        public ChessMatch() {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            PlacePieces();
            Check = false;
        }

        public Piece ExecuteMoviment(Position origin, Position destiny)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncrementAmountOfMoviments();
            Piece CapturedPiece = Board.RemovePiece(destiny);
            Board.PlacePiece(p, destiny);
            if (CapturedPiece != null)
            {
                Captured.Add(CapturedPiece);
            }

            return CapturedPiece;
        }

        public void UndoMoviment(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece p = Board.RemovePiece(destiny);
            p.DecrementAmountOfMoviments();
            if (capturedPiece != null)
            {
                Board.PlacePiece(capturedPiece, destiny);
                Captured.Remove(capturedPiece);
            }

            Board.PlacePiece(p, origin);
        }

        public void PerformMove(Position origin, Position destiny)
        {
            Piece capturedPiece = ExecuteMoviment(origin, destiny);

            if(isInCheck(CurrentPlayer))
            {
                UndoMoviment(origin, destiny, capturedPiece);
                throw new BoardException("Você não pode se colocar em xeque!");
            }

            if(isInCheck(Adversary(CurrentPlayer)))
            {
                Check = true;
            } else
            {
                Check = false;
            }

            if(TestCheckMate(Adversary(CurrentPlayer)))
            {
                Finished = true;
            } else
            {
                Turn++;
                ChangePlayer();
            }
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

        private static Color Adversary(Color color)

        {
            if(color == Color.White)
            {
                return Color.Black;
            } else
            {
                return Color.White;
            }
        }

        private Piece King(Color color)
        {
            foreach (Piece x in PiecesInGame(color))
            {
                if(x is King)
                {
                    return x;
                }
            }

            return null;
        }

        public bool isInCheck(Color color)
        {
            Piece king = King(color) ?? throw new BoardException($"Não tem rei da cor {color} no tabuleiro ");

            foreach (Piece x in PiecesInGame(Adversary(color)))
            {
                bool[,] mat = x.PossibleMoviments();
                if (mat[king.Position.Row, king.Position.Column])
                {
                    return true;
                }
            }

            return false;
        }

        public bool TestCheckMate(Color color)
        {
            if (!isInCheck(color))
            {
                return false;
            }

            foreach(Piece x in PiecesInGame(color))
            {
                bool[,] mat = x.PossibleMoviments();
                for (int i = 0; i < Board.Rows; i++)
                {
                    for(int j = 0; j < Board.Columns; j++)
                    {
                        if (mat[i, j] = true)
                        {
                            Position origin = x.Position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = ExecuteMoviment(origin, destiny);
                            bool testCheck = isInCheck(color);
                            UndoMoviment(origin, destiny, capturedPiece);
                            if(!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
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
