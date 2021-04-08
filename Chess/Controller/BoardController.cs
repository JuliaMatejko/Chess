using Chess.Model;
using System;

namespace Chess.Controller
{
    class BoardController // nie do konca rozumiem czym jest controller, doczytac, poprzenosić members jesli trzeba
    {
        public static void MakeAMove()
        {
            Console.Write(" Make a move: ");
            string chosenMove = Console.ReadLine();

            Move move = StringToMove(chosenMove);
            while (!MoveValidator.MoveIsPossible(move))
            {
                Console.WriteLine($" It's not a valid move. Choose a differnt piece or/and field: "); //to do --> more specific?
                chosenMove = Console.ReadLine();
            }
            Game.Fields[move.NewPosition].Content = Game.Fields[move.CurrentPosition].Content;
            Game.Fields[move.CurrentPosition].Content = null;
        }

        static Move StringToMove(string str)
        {
            string[] substrings = str.Split(" ");
            return new Move(substrings[0], substrings[1], substrings[2]);
        }
    }     
}
