using Chess.Model;
using System;

namespace Chess.View
{
    class BoardView
    {
        public static void PrintBoard(Board board)
        {
            Console.WriteLine("");
            Console.WriteLine("   +---------------------------------------+");
            for (var j = 7; j >= 0; j--)
            {
                Console.Write($" {j + 1} |");
                for (var i = 0; i <= 7; i++)
                {
                    if (i != 7)
                    {
                        if (!(board[i][j].Content == null))
                        {
                            Console.Write($" {board[i][j].Content.Name} |");
                        }
                        else
                        {
                            Console.Write("    |");
                        }
                    }
                    else
                    {
                        if (!(board[i][j].Content == null))
                        {
                            Console.WriteLine($" {board[i][j].Content.Name} |");
                        }
                        else
                        {
                            Console.WriteLine("    |");
                        }
                    }
                }
                if (j != 0)
                {
                    Console.WriteLine("   |----+----+----+----+----+----+----+----|");
                }
                else
                {
                    Console.WriteLine("   +---------------------------------------+");
                    Console.WriteLine("     a    b    c    d    e    f    g    h   ");
                }
            }
            Console.WriteLine("");
        }
    }
}
