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
                ChessMatch match = new();

                while (!match.Finished)
                {
                    try
                    {
                        Console.Clear();
                        Screen.PrintBoard(match.Board);
                        Console.WriteLine();
                        Console.WriteLine($"Turno: {match.Turn}");
                        Console.WriteLine($"Aguardando jogada: {match.CurrentPlayer}");

                        Console.WriteLine();
                        Console.Write("Digite a posição de origem: ");
                        Position origin = Screen.ReadChessPosition().ConvertToPosition();
                        match.ValidateOriginPosition(origin);

                        bool[,] PossiblePositions = match.Board.Piece(origin).PossibleMoviments();

                        Console.Clear();
                        Screen.PrintBoard(match.Board, PossiblePositions);

                        Console.WriteLine();
                        Console.Write("Digite a posição de destino: ");
                        Position destiny = Screen.ReadChessPosition().ConvertToPosition();
                        match.ValidateDestinyPosition(origin, destiny);

                        match.PerformMove(origin, destiny);
                    } catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }

            }

            catch (BoardException e) {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}