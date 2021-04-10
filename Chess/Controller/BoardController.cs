using Chess.Model;
using Chess.Model.Pieces;
using System;

namespace Chess.Controller
{
    class BoardController // nie do konca rozumiem czym jest controller, doczytac, poprzenosić members jesli trzeba
    {
        public static void MakeAMove()
        {
            Console.Write($"{GameState.CurrentPlayer} turn, make a move: ");
            string chosenMove = Console.ReadLine(); //to do: validate string (only take format 'piece current position next position'

            Move move = StringToMove(chosenMove);
            Piece piece = FindPiece(move.PieceName, move.CurrentPosition);

            while (!MoveValidator.MoveIsPossible(piece, move))
            {
                Console.Write($" It's not a valid move. Choose a differnt piece or/and field: "); //to do --> more specific? uzyj zmiennej w miejsce komunikatu o bledzie
                chosenMove = Console.ReadLine();
                move = StringToMove(chosenMove);
                piece = FindPiece(move.PieceName, move.CurrentPosition);
            }
            if (piece.GetType() == typeof(Pawn)) //works?
            {
                Pawn pawn = (Pawn)piece;
                if (pawn.IsFirstMove)
                {
                    pawn.IsFirstMove = false;
                }
            }
            Game.Fields[move.NewPosition].Content = Game.Fields[move.CurrentPosition].Content;
            Game.Fields[move.CurrentPosition].Content = null;
        }

        static Move StringToMove(string str)
        {
            if (string.IsNullOrEmpty(str))
            {

            }
            string[] substrings = str.Split(" ");
            return new Move(substrings[0], substrings[1], substrings[2]);
        }

        static Piece FindPiece(string piecename, string currentposition)
        {
            Piece piece = Game.Fields[currentposition].Content;
            
            if (piece == null) // empty field
            {
                return null;
            }
            else if (piece.Name == piecename) // found piece
            {
                return piece;
            }
            return null; // found different piece on chosen position
        }
    }     
}
