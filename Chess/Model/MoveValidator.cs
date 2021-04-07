using Chess.Model;

namespace Chess
{
    class MoveValidator
    {
        public static bool MoveIsPossible(Move move) // ograniczyć rucszanie nie swoimi bierkami
        {
            if (true)//MoveIsValid(move) && MoveIsLegal(move))
            {
                return true;
            }
            return false;
        }

        /*
        bool MoveIsLegal(Move move)
        {
            if (MoveIsPrinciplePieceMove(move) && !ChosenPieceProtectsKingCheck(move))
            {
                return true;
            }
            return false;
        }

        bool MoveIsPrinciplePieceMove(Move move) //! .... TO DO
        {
            if (NextAvailablePositions.Contains(move.field))
            {
                return true;
            }
            return false;
            //return true;
        }

        void ReturnAvailablePieceMoves(Move move)
        {
            List<string> availablePieceMoves = new List<string>();
            //pawn
            //MoveOneForward();
            //MoveTwoForward();


        }

        bool ChosenPieceProtectsKingCheck(Move move) //! .... TO DO
        {
                if (true)
                {
                    return true;
                }
                return false;
                return false;
            }

        bool MoveIsValid(Move move)
        {
            if (ChosenPieceIsValid(Convert.ToString(move.PieceName)) && ChosenFieldIsValid(Convert.ToString(move.NewField)))
            {
                return true;
            }
            return false;
        }

        bool ChosenFieldIsValid(string field) // chosen field is on the board
        {
            if (Positions.Contains(field))
            {
                return true;
            }
            return false;
        }

        bool ChosenPieceIsValid(string piece) // chosen piece is a chess piece
        {
            if (Piece.PieceNames.Contains(piece))
            {
                return true;
            }
            return false;
        }
        */
        //to do end

    }
}
