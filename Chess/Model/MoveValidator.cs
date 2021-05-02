using Chess.Model;
using Chess.Model.Pieces;
using System;

namespace Chess
{
    class MoveValidator
    { 
        public static bool MoveIsPossible(Piece piece, Move move)
        {
            if (PieceOnChosenPositionDoesExist(piece) 
                && ChosenPieceIsCurrentPlayersPiece(piece))
            {
                if (piece.GetType() == typeof(King))
                {
                    return MoveIsCorrectKingMove(piece, move);
                }
                else
                {
                    return MoveIsCorrectPieceMove(piece, move);
                } 
            }
            return false;
        }
        
        static bool MoveIsCorrectPieceMove(Piece piece, Move move)
        {
            return piece.NextAvailablePositions.Contains(move.NewPosition);
        }

        static bool MoveIsCorrectKingMove(Piece king, Move move)
        {
            if (king.NextAvailablePositions.Contains(move.NewPosition) 
                && KingNewPositionIsSafe(move.NewPosition))
            {
                int squaresMoved = Array.IndexOf(Board.Files, move.NewPosition.Substring(0, 1))
                                   - Array.IndexOf(Board.Files, move.CurrentPosition.Substring(0, 1));
                if (Math.Abs(squaresMoved) == 2) // king castling
                {
                    return CastlingIsPossible(squaresMoved, king.IsWhite);
                }
                else
                {
                    return true;
                }
            }
            return false;

            static bool CastlingIsPossible(int squaresmoved, bool iswhite)
            {
                string[] positions = iswhite ? new string[] { "f1", "d1" } : new string[] { "f8", "d8" };
                if (squaresmoved == 2)
                {
                    return KingNewPositionIsSafe(positions[0]);
                }
                else // squaresmoved == -2
                {
                    return KingNewPositionIsSafe(positions[1]);
                }
            }
        }

        public static bool KingNewPositionIsSafe(string newposition)
        {
            for (int i = 0; i < Board.boardSize; i++)
            {
                for (int j = 0; j < Board.boardSize; j++)
                {
                    Piece piece = Game.board[i][j].Content;
                    if (piece != null)
                    {
                        bool isOponentsPiece = GameState.CurrentPlayer == GameState.Sides.White ? !piece.IsWhite : piece.IsWhite;
                        if (isOponentsPiece)
                        {
                            if (piece.GetType() == typeof(Pawn))
                            {
                                Pawn pawn = (Pawn)piece;
                                if (pawn.ControlledSquares.Contains(newposition))
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                if (piece.NextAvailablePositions.Contains(newposition))
                                {
                                    return false;
                                }
                            } 
                        }
                    }
                }
            }
            return true;
        }
        
        static bool ChosenPieceIsCurrentPlayersPiece(Piece piece)
        {
            if (piece.IsWhite && GameState.CurrentPlayer == GameState.Sides.White)
            {
                return true;
            }
            else if (!(piece.IsWhite) && GameState.CurrentPlayer == GameState.Sides.Black)
            {
                return true;
            }
            return false;
        }

        static bool PieceOnChosenPositionDoesExist(Piece piece)
        {
            return piece != null;
        }
    }
}
