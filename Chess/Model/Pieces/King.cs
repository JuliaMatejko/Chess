using System.Collections.Generic;

namespace Chess.Model.Pieces
{
    class King : Piece
    {
        public bool IsFirstMove { get; set; } = true;
        static public new string[] PieceNames => new string[] { "Kw", "Kb" };

        public King(bool iswhite, string position)
        {
            IsWhite = iswhite;
            Position = position;
            Name = iswhite == true ? Name = PieceNames[0] : Name = PieceNames[1];
        }

        protected override HashSet<string> ReturnCorrectPieceMoves(int fileIndex, int rankIndex, Board board, HashSet<string> positions)
        {
            KingMove(fileIndex, rankIndex, board, positions);
            CastlingMove(fileIndex, rankIndex, board, positions);
            return positions;
        }

        HashSet<string> CastlingMove(int fileIndex, int rankIndex, Board board, HashSet<string> positions)
        {
            bool kingIsNotInCheck = IsWhite ? !GameState.WhiteKingIsInCheck : !GameState.BlackKingIsInCheck;
            if (IsFirstMove && kingIsNotInCheck)
            {
                Piece[] pieces = IsWhite ? new Piece[] { board[7][0].Content, board[0][0].Content }
                                         : new Piece[] { board[7][7].Content, board[0][7].Content };
                if (pieces[0] != null && pieces[0].GetType() == typeof(Rook))
                {
                    Rook rook = (Rook)pieces[0];
                    if (rook.IsFirstMove)
                    {
                        CastleKingSide();
                    }
                }
                if (pieces[1] != null && pieces[1].GetType() == typeof(Rook))
                {
                    Rook rook = (Rook)pieces[1];
                    if (rook.IsFirstMove)
                    {
                        CastleQueenSide();
                    }
                }
            }
            return positions;

            void CastleKingSide() => CastleKingMove(2, fileIndex, rankIndex);
            void CastleQueenSide() => CastleKingMove(-2, fileIndex, rankIndex);
            void CastleKingMove(int x, int file, int rank)
            {
                int z = x == 2 ? 1 : -1;
                if (board[file + z][rank].Content == null && board[file + x][rank].Content == null)
                {
                    if (KingNewPositionIsSafe(Board.Files[file + x] + Board.Ranks[rank]))
                    {
                        positions.Add(Board.Files[file + x] + Board.Ranks[rank]);
                    } 
                }
            }
        }

        bool KingNewPositionIsSafe(string newposition) // not optimal to do: make array of attacked squares?
        {
            for (int i = 0; i < Board.boardSize; i++)
            {
                for (int j = 0; j < Board.boardSize; j++)
                {
                    Piece piece = Game.board[i][j].Content;
                    if (piece != null)
                    {
                        bool isOponentsPiece = IsWhite ? !piece.IsWhite : piece.IsWhite;
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

        HashSet<string> KingMove(int fileIndex, int rankIndex, Board board, HashSet<string> positions)
        {
            if (rankIndex < Board.boardSize - 1)
            {
                if (IsWhite)
                {
                    MoveOneForward();
                    if (fileIndex == 0)
                    {
                        MoveOneRight();
                        MoveOneForwardDiagonallyRight();
                    }
                    else if (fileIndex == 7)
                    {
                        MoveOneLeft();
                        MoveOneForwardDiagonallyLeft();
                    }
                    else
                    {
                        MoveOneRight();
                        MoveOneLeft();
                        MoveOneForwardDiagonallyLeft();
                        MoveOneForwardDiagonallyRight();
                    }
                }
                else
                {
                    MoveOneBackwards();
                    if (fileIndex == 0)
                    {
                        MoveOneLeft();
                        MoveOneBackwardsDiagonallyLeft();

                    }
                    else if (fileIndex == 7)
                    {
                        MoveOneRight();
                        MoveOneBackwardsDiagonallyRight();
                    }
                    else
                    {
                        MoveOneRight();
                        MoveOneLeft();
                        MoveOneBackwardsDiagonallyLeft();
                        MoveOneBackwardsDiagonallyRight();
                    }
                }
            }
            if (rankIndex > 0)
            {
                if (IsWhite)
                {
                    MoveOneBackwards();
                    if (fileIndex == 0)
                    {
                        MoveOneRight();
                        MoveOneBackwardsDiagonallyLeft();
                    }
                    else if (fileIndex == 7)
                    {
                        MoveOneLeft();
                        MoveOneBackwardsDiagonallyRight();
                    }
                    else
                    {
                        MoveOneRight();
                        MoveOneLeft();
                        MoveOneBackwardsDiagonallyLeft();
                        MoveOneBackwardsDiagonallyRight();
                    }
                }
                else
                {
                    MoveOneForward();
                    if (fileIndex == 0)
                    {
                        MoveOneLeft();
                        MoveOneForwardDiagonallyLeft();
                    }
                    else if (fileIndex == 7)
                    {
                        MoveOneRight();
                        MoveOneForwardDiagonallyRight();
                    }
                    else
                    {
                        MoveOneRight();
                        MoveOneLeft();
                        MoveOneForwardDiagonallyLeft();
                        MoveOneForwardDiagonallyRight();
                    }
                }
            }
            return positions;
            void MoveOneForwardDiagonallyLeft() => MoveKing(-1, 1, fileIndex, rankIndex, board, positions);
            void MoveOneBackwardsDiagonallyLeft() => MoveKing(-1, -1, fileIndex, rankIndex, board, positions);
            void MoveOneForwardDiagonallyRight() => MoveKing(1, 1, fileIndex, rankIndex, board, positions);
            void MoveOneBackwardsDiagonallyRight() => MoveKing(1, -1, fileIndex, rankIndex, board, positions);
            void MoveOneForward() => MoveKing(0, 1, fileIndex, rankIndex, board, positions);
            void MoveOneBackwards() => MoveKing(0, -1, fileIndex, rankIndex, board, positions);
            void MoveOneRight() => MoveKing(1, 0, fileIndex, rankIndex, board, positions);
            void MoveOneLeft() => MoveKing(-1, 0, fileIndex, rankIndex, board, positions);
        }

        void MoveKing(int x_white, int y_white, int fileIndex, int rankIndex, Board board, HashSet<string> positions)
        {
            int x = IsWhite ? x_white : -x_white;
            int y = IsWhite ? y_white : -y_white;
            Field newField = board[fileIndex + x][rankIndex + y];

            if (newField.Content == null)
            {
                if (KingNewPositionIsSafe(newField.Name))
                {
                    positions.Add(newField.Name);
                }
            }
            else
            {
                bool z = IsWhite ? !(newField.Content.IsWhite) : newField.Content.IsWhite;
                if (z && newField.Content.GetType() != typeof(King))
                {
                    if (KingNewPositionIsSafe(newField.Name))
                    {
                        positions.Add(newField.Name);
                    }
                }
            } 
        }
    }
}
