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
            for (int j = 7; j >= 0; j--)
            {
                Console.Write($" {j + 1} |");
                for (int i = 0; i <= 7; i++)
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

        public static void PrintBoardLayout()
        {
            Console.WriteLine("   +---------------------------------------+");
            Console.WriteLine(" 8 | Rb | kb | Bb | Qb | Kb | Bb | kb | Rb |");
            Console.WriteLine("   |----+----+----+----+----+----+----+----|");
            Console.WriteLine(" 7 | pb | pb | pb | pb | pb | pb | pb | pb |");
            Console.WriteLine("   |----+----+----+----+----+----+----+----|");
            Console.WriteLine(" 6 |    |    |    |    |    |    |    |    |");
            Console.WriteLine("   |----+----+----+----+----+----+----+----|");
            Console.WriteLine(" 5 |    |    |    |    |    |    |    |    |");
            Console.WriteLine("   |----+----+----+----+----+----+----+----|");
            Console.WriteLine(" 4 |    |    |    |    |    |    |    |    |");
            Console.WriteLine("   |----+----+----+----+----+----+----+----|");
            Console.WriteLine(" 3 |    |    |    |    |    |    |    |    |");
            Console.WriteLine("   |----+----+----+----+----+----+----+----|");
            Console.WriteLine(" 2 | pw | pw | pw | pw | pw | pw | pw | pw |");
            Console.WriteLine("   |----+----+----+----+----+----+----+----|");
            Console.WriteLine(" 1 | Rw | kw | Bw | Qw | Kw | Bw | kw | Rw |");
            Console.WriteLine("   +---------------------------------------+");
            Console.WriteLine("     a    b    c    d    e    f    g    h   ");
        }
    }
}
