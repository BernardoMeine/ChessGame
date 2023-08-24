using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Section12ChessGame.Entities.BoardClasses;
using Section12ChessGame.Entities.ChessClasses;

namespace Section12ChessGame.Entities;

internal class Screen
{
    public static void PrintBoard (Board board)
    {
        for(int i = 0; i < board.Rows; i++)
        {
            Console.Write(8 - i + " ");
            for (int j = 0; j < board.Columns; j++)
            {
               PrintPiece(board.Piece(i, j));
            }
            Console.WriteLine();
        }

        Console.WriteLine("  a b c d e f g h");
    }

    public static void PrintBoard(Board board, bool[,] PossiblePositions)
    {
        ConsoleColor OriginalBackground = Console.BackgroundColor;
        ConsoleColor NewBackground = ConsoleColor.DarkGray;

        for (int i = 0; i < board.Rows; i++)
        {
            Console.Write(8 - i + " ");
            for (int j = 0; j < board.Columns; j++)
            {
                if (PossiblePositions[i, j])
                {
                    Console.BackgroundColor = NewBackground;
                }
                else
                {
                    Console.BackgroundColor = OriginalBackground;
                }
                PrintPiece(board.Piece(i, j));
                Console.BackgroundColor = OriginalBackground;
            }
            Console.WriteLine();
        }

        Console.WriteLine("  a b c d e f g h");
        Console.BackgroundColor = OriginalBackground;
    }

    public static ChessPosition ReadChessPosition()
    {
        string s = Console.ReadLine();
        char column = s[0];
        int row = int.Parse(s[1] + " ");
        return new ChessPosition(column, row);
    }

    public static void PrintPiece(Piece piece)
    {
         if (piece == null) {
             Console.Write("- ");
         } else { 

             if (piece.Color == Color.White)
             {
               Console.Write(piece);
             } else
             {
              ConsoleColor aux = Console.ForegroundColor;
              Console.ForegroundColor = ConsoleColor.DarkBlue;
              Console.Write(piece);
              Console.ForegroundColor = aux;
             }
            Console.Write(" ");
        }
    }

    public static void PrintCapturedPieces(ChessMatch match)
    {
        Console.WriteLine("Peças capturadas: ");
        Console.Write("Brancas: ");
        PrintHashSet(match.CapturedPieces(Color.White));
        Console.WriteLine();
        Console.Write("Pretas: ");
        ConsoleColor aux = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        PrintHashSet(match.CapturedPieces(Color.Black));
        Console.ForegroundColor = aux;
        Console.WriteLine();
    }

    public static void PrintHashSet(HashSet<Piece> hashSet)
    {
        Console.Write("[");
        foreach (Piece piece in hashSet)
        {
            Console.Write(piece + " ");
        }
        Console.Write("]");
    }

    public static void PrintMatch(ChessMatch match)
    {
        PrintBoard(match.Board);
        Console.WriteLine();
        PrintCapturedPieces(match);
        Console.WriteLine();
        Console.WriteLine($"Turno: {match.Turn}");
        Console.WriteLine($"Aguardando jogada: {match.CurrentPlayer}");
    }
}
