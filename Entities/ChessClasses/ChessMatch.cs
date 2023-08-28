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
        public Piece VulnerableEnPassant { get; private set; }


        public ChessMatch() {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            PlacePieces();
            Check = false;
            VulnerableEnPassant = null;
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

            //Special move Small Roq

            if (p is King && destiny.Column == origin.Column + 2)
            {
                Position towerOrigin = new Position(origin.Row, origin.Column + 3);
                Position towerDestiny = new Position(origin.Row, origin.Column + 1);
                Piece T = Board.RemovePiece(towerOrigin);
                T.IncrementAmountOfMoviments();
                Board.PlacePiece(T, towerDestiny);
            }

            //Special move Big Roq

            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position towerOrigin = new Position(origin.Row, origin.Column - 4);
                Position towerDestiny = new Position(origin.Row, origin.Column - 1);
                Piece T = Board.RemovePiece(towerOrigin);
                T.IncrementAmountOfMoviments();
                Board.PlacePiece(T, towerDestiny);
            }

            //# Special Move En Passant
            if (p is Pawn)
            {
                if (origin.Column != destiny.Column && CapturedPiece == null) {
                    Position pawnPos;
                    if(p.Color == Color.White)
                    {
                        pawnPos = new Position(destiny.Row + 1, destiny.Column);
                    } else
                    {
                        pawnPos = new Position(destiny.Row - 1, destiny.Column);
                    }
                    CapturedPiece = Board.RemovePiece(pawnPos);
                    Captured.Add(CapturedPiece);
                }
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

            //Special move Small Roq

            if (p is King && destiny.Column == origin.Column + 2)
            {
                Position towerOrigin = new Position(origin.Row, origin.Column + 3);
                Position towerDestiny = new Position(origin.Row, origin.Column + 1);
                Piece T = Board.RemovePiece(towerDestiny);
                T.DecrementAmountOfMoviments();
                Board.PlacePiece(T, towerOrigin);
            }

            //Special move Big Roq

            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position towerOrigin = new Position(origin.Row, origin.Column - 4);
                Position towerDestiny = new Position(origin.Row, origin.Column - 1);
                Piece T = Board.RemovePiece(towerDestiny);
                T.DecrementAmountOfMoviments();
                Board.PlacePiece(T, towerOrigin);
            }

            //# Special Move En Passant
            if (p is Pawn)
            {
                if (origin.Column != destiny.Column && capturedPiece == VulnerableEnPassant)
                {
                    Piece pawn = Board.RemovePiece(destiny);
                    Position pawnPos;
                    if (p.Color == Color.White)
                    {
                        pawnPos = new Position(3, destiny.Column);
                    } else
                    {
                        pawnPos = new Position(4, destiny.Column);
                    }
                    Board.PlacePiece(p, origin);
                }
            }
        }

        public void PerformMove(Position origin, Position destiny)
        {
            Piece capturedPiece = ExecuteMoviment(origin, destiny);

            if(IsInCheck(CurrentPlayer))
            {
                UndoMoviment(origin, destiny, capturedPiece);
                throw new BoardException("Você não pode se colocar em xeque!");
            }

            Piece p = Board.Piece(destiny);

            // #Special Move Promotion
            if(p is Pawn)
            {
                if((p.Color == Color.White && destiny.Row == 0) || (p.Color == Color.Black && destiny.Row == 7))
                {
                    p = Board.RemovePiece(destiny);
                    Pieces.Remove(p);
                    Piece Queen = new Queen(p.Color,Board);
                    Board.PlacePiece(Queen, destiny);
                    Pieces.Add(Queen);
                }
            }

            if (IsInCheck(Adversary(CurrentPlayer)))
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

            // # Special Move En Passant

            if(p is Pawn && (destiny.Row == origin.Row - 2 || destiny.Row == origin.Row + 2))
            {
                VulnerableEnPassant = p;
            } else
            {
                VulnerableEnPassant = null;
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
            if (!Board.Piece(origin).PossibleMoviment(destiny)) {
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

        public bool IsInCheck(Color color)
        {
            Piece king = King(color);

            if(king == null)
            {
                throw new BoardException("Não existe rei da cor informada!");
            }

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
            if (!IsInCheck(color))
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
                        if (mat[i, j] == true)
                        {
                            Position origin = x.Position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = ExecuteMoviment(origin, destiny);
                            bool testCheck = IsInCheck(color);
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
            PlaceNewPiece('a', 1, new Tower(Color.White, Board));
            PlaceNewPiece('b', 1, new Horse(Color.White, Board));
            PlaceNewPiece('c', 1, new Bishop(Color.White, Board));
            PlaceNewPiece('d', 1, new Queen(Color.White, Board));
            PlaceNewPiece('e', 1, new King(Color.White, Board, this));
            PlaceNewPiece('f', 1, new Bishop(Color.White, Board));
            PlaceNewPiece('g', 1, new Horse(Color.White, Board));
            PlaceNewPiece('h', 1, new Tower(Color.White, Board));
            PlaceNewPiece('a', 2, new Pawn(Color.White, Board, this));
            PlaceNewPiece('b', 2, new Pawn(Color.White, Board, this));
            PlaceNewPiece('c', 2, new Pawn(Color.White, Board, this));
            PlaceNewPiece('d', 2, new Pawn(Color.White, Board, this));
            PlaceNewPiece('e', 2, new Pawn(Color.White, Board, this));
            PlaceNewPiece('f', 2, new Pawn(Color.White, Board, this));
            PlaceNewPiece('g', 2, new Pawn(Color.White, Board, this));
            PlaceNewPiece('h', 2, new Pawn(Color.White, Board, this));



            PlaceNewPiece('a', 8, new Tower(Color.Black, Board));
            PlaceNewPiece('b', 8, new Horse(Color.Black, Board));
            PlaceNewPiece('c', 8, new Bishop(Color.Black, Board));
            PlaceNewPiece('d', 8, new Queen(Color.Black, Board));
            PlaceNewPiece('e', 8, new King(Color.Black, Board, this));
            PlaceNewPiece('f', 8, new Bishop(Color.Black, Board));
            PlaceNewPiece('g', 8, new Horse(Color.Black, Board));
            PlaceNewPiece('h', 8, new Tower(Color.Black, Board));
            PlaceNewPiece('a', 7, new Pawn(Color.Black, Board, this));
            PlaceNewPiece('b', 7, new Pawn(Color.Black, Board, this));
            PlaceNewPiece('c', 7, new Pawn(Color.Black, Board, this));
            PlaceNewPiece('d', 7, new Pawn(Color.Black, Board, this));
            PlaceNewPiece('e', 7, new Pawn(Color.Black, Board, this));
            PlaceNewPiece('f', 7, new Pawn(Color.Black, Board, this));
            PlaceNewPiece('g', 7, new Pawn(Color.Black, Board, this));
            PlaceNewPiece('h', 7, new Pawn(Color.Black, Board, this));
        }

        
    }
}
