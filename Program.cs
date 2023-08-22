using Section12ChessGame.Entities;
using Section12ChessGame.Entities.BoardClasses;

namespace Section12ChessGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Board board = new(8, 8);

            Screen.PrintBoard(board);
        }
    }
}