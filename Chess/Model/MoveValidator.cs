using Chess.Model;
using Chess.Model.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace Chess
{
    class MoveValidator // service which role is validating moves 
    {
        public static bool MoveIsPossible(Move move)//, Piece piece) // ograniczyć rucszanie nie swoimi bierkami
        {
            if (true)//(MoveIsValid(move) && MoveIsLegal(move, piece))
            {
                return true;
            }
            return false;
        }

        static bool MoveIsLegal(Move move, Piece piece)
        {
            if (MoveIsPrinciplePieceMove(move, piece) && !AbsolutePin(move))
            {
                return true;
            }
            return false;
        }

        static bool MoveIsValid(Move move)
        {
            if (ChosenPieceIsValid(move.PieceName) && ChosenFieldIsValid(move.NewPosition))
            {
                return true;
            }
            return false;
        }

        static bool MoveIsPrinciplePieceMove(Move move, Piece piece) //! .... TO DO
        {
            if (piece.NextAvailablePositions.Contains(move.NewPosition))
            {
                return true;
            }
            return false;
            //return true;
        }

        static bool AbsolutePin(Move move) //! .... TO DO the pinned piece cannot legally move out of the line of attack (as moving it would expose the king to check).
        {
                if (true)
                {
                    return true;
                }
                return false;
            }

        static bool ChosenFieldIsValid(string field) // chosen field is on the board
        {
            if (Game.Positions.Contains(field))
            {
                return true;
            }
            return false;
        }

        static bool ChosenPieceIsValid(string piece) // chosen piece is a chess piece
        {
            if (Piece.PieceNames.Contains(piece))
            {
                return true;
            }
            return false;
        }
    }
}
