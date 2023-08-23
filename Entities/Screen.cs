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
                if (board.Piece(i, j) == null)
                {
                    Console.Write("- ");
                }
                else
                {
                    PrintPiece(board.Piece(i, j));
                    Console.Write(" ");
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine("  a b c d e f g h");
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
        if(piece.Color == Color.White)
        {
            Console.Write(piece);
        } else
        {
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(piece);
            Console.ForegroundColor = aux;
        }
    }
}
