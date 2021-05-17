using Chess.Model;
using Chess.Model.Pieces;

namespace Chess
{
    class MoveValidator
    { 
        public static bool MoveIsPossible(Piece piece, Move move)
        {
            if (piece != null && ChosenPieceIsCurrentPlayersPiece(piece))
            {
                return MoveIsCorrectPieceMove(piece, move);
            }
            return false;
        }

        private static bool MoveIsCorrectPieceMove(Piece piece, Move move)
        {
            return piece.NextAvailablePositions.Contains(move.NewPosition);
        }

        private static bool ChosenPieceIsCurrentPlayersPiece(Piece piece)
        {
            return (piece.IsWhite && GameState.CurrentPlayer == GameState.Sides.White)
                    || (!piece.IsWhite && GameState.CurrentPlayer == GameState.Sides.Black);
        }
    }
}
