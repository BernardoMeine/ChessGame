using Section12ChessGame.Entities;
using Section12ChessGame.Entities.BoardClasses;
using Section12ChessGame.Entities.ChessClasses;

namespace Section12ChessGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Board board = new(8, 8);


                board.PlacePiece(new King(Color.White, board), new Position(0, 2));
                board.PlacePiece(new King(Color.Black, board), new Position(0, 4));
                board.PlacePiece(new King(Color.Blue, board), new Position(3, 6));
                board.PlacePiece(new Tower(Color.Red, board), new Position(4, 3));
                board.PlacePiece(new Tower(Color.Green, board), new Position(4, 3));



                Screen.PrintBoard(board);
            }

            catch (BoardException e) {
                Console.WriteLine($"Board error: {e.Message}");
            }
            
        }
    }
}