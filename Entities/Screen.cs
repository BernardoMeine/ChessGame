using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Section12ChessGame.Entities.BoardClasses;

namespace Section12ChessGame.Entities;

internal class Screen
{
    public static void PrintBoard (Board board)
    {
        for(int i = 0; i < board.Rows; i++)
        {
            for(int j = 0; j < board.Columns; j++)
            {
                if (board.Piece(i, j) == null)
                {
                    Console.Write("- ");
                }
                else
                {
                    Console.Write(board.Piece(i, j) + " ");
                }
            }
            Console.WriteLine();
        }
    }
}
