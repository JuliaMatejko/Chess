using Chess.Model;
using Chess.Model.Pieces;
using System;
using System.Linq;

namespace Chess.Controller
{
    class BoardController
    {
        public static void MakeAMove()
        {
            Console.Write($" {GameState.CurrentPlayer} turn, make a move: ");
            string chosenMove = Console.ReadLine();

            while (!UserInputIsValid(chosenMove))
            {
                Console.Write($" It's not a valid move. Choose a differnt piece or/and field: ");
                chosenMove = Console.ReadLine();
            }
            Move move = StringToMove(chosenMove);
            Piece piece = FindPiece(move.PieceName, move.CurrentPosition);

            while (!MoveValidator.MoveIsPossible(piece, move))
            {
                Console.Write($" It's not a valid move. Choose a differnt piece or/and field: ");
                chosenMove = Console.ReadLine();
                move = StringToMove(chosenMove);
                piece = FindPiece(move.PieceName, move.CurrentPosition);
            }
            if (piece.GetType() == typeof(Pawn))
            {
                Pawn pawn = (Pawn)piece;
                if (pawn.IsFirstMove)
                {
                    pawn.IsFirstMove = false;
                }
            }
            if (piece.GetType() == typeof(King))
            {
                King king = (King)piece;
                if (king.IsFirstMove)
                {
                    king.IsFirstMove = false;
                }
            }
            if (piece.GetType() == typeof(Rook))
            {
                Rook rook = (Rook)piece;
                if (rook.IsFirstMove)
                {
                    rook.IsFirstMove = false;
                }
            }
            piece.Position = move.NewPosition;
            Game.Fields[move.NewPosition].Content = Game.Fields[move.CurrentPosition].Content;
            Game.Fields[move.CurrentPosition].Content = null;
        }

        static bool UserInputIsValid(string chosenmove)
        {
            if (chosenmove.Length == 8 && chosenmove[2] == ' ' && chosenmove[5] == ' ')
            {
                Move move = StringToMove(chosenmove);

                if (Piece.PieceNames.Contains(move.PieceName)
                    && Board.Positions.Contains(move.CurrentPosition)
                    && Board.Positions.Contains(move.CurrentPosition))
                {
                    return true;
                }
            }
            return false;
        }

        static Move StringToMove(string str)
        {
            string[] substrings = str.Split(" ");
            return new Move(substrings[0], substrings[1], substrings[2]);
        }

        static Piece FindPiece(string piecename, string currentposition)
        {
            Piece piece = Game.Fields[currentposition].Content;
            
            if (piece == null)
            {
                return null;
            }
            else if (piece.Name == piecename)
            {
                return piece;
            }
            return null;
        }
    }     
}
