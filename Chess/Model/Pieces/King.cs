using System.Collections.Generic;

namespace Chess.Model.Pieces
{
    class King : Piece
    {
        public bool IsFirstMove { get; set; } = true;
        private static new string[] PieceNames { get; } = new string[] { "Kw", "Kb" };

        public King(bool isWhite, string position)
        {
            IsWhite = isWhite;
            Position = position;
            Name = isWhite == true ? Name = PieceNames[0] : Name = PieceNames[1];
        }

        protected override HashSet<string> ReturnCorrectPieceMoves(int fileIndex, int rankIndex, Board board, HashSet<string> positions)
        {
            KingMove(fileIndex, rankIndex, board, positions);
            CastlingMove(fileIndex, rankIndex, board, positions);
            return positions;
        }

        private HashSet<string> CastlingMove(int fileIndex, int rankIndex, Board board, HashSet<string> positions)
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
                    string kingNewPosition = Board.Files[file + x] + Board.Ranks[rank];
                    if (KingNewPositionIsSafe(kingNewPosition))
                    {
                        positions.Add(kingNewPosition);
                    }
                }
            }
        }

        private bool KingNewPositionIsSafe(string newPosition)
        {
            for (var i = 0; i < Board.BoardSize; i++)
            {
                for (var j = 0; j < Board.BoardSize; j++)
                {
                    Piece piece = Game.Board[i][j].Content;
                    if (piece != null)
                    {
                        bool isOponentsPiece = IsWhite ? !piece.IsWhite : piece.IsWhite;
                        if (isOponentsPiece && piece.ControlledSquares.Contains(newPosition))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private HashSet<string> KingMove(int fileIndex, int rankIndex, Board board, HashSet<string> positions)
        {
            if (rankIndex < Board.BoardSize - 1)
            {
                if (IsWhite)
                {
                    MoveOneForward();
                    switch (fileIndex)
                    {
                        case 0:
                            MoveOneRight();
                            MoveOneForwardDiagonallyRight();
                            break;
                        case 7:
                            MoveOneLeft();
                            MoveOneForwardDiagonallyLeft();
                            break;
                        default:
                            MoveOneRight();
                            MoveOneLeft();
                            MoveOneForwardDiagonallyLeft();
                            MoveOneForwardDiagonallyRight();
                            break;
                    }
                }
                else
                {
                    MoveOneBackwards();
                    switch (fileIndex)
                    {
                        case 0:
                            MoveOneLeft();
                            MoveOneBackwardsDiagonallyLeft();
                            break;
                        case 7:
                            MoveOneRight();
                            MoveOneBackwardsDiagonallyRight();
                            break;
                        default:
                            MoveOneRight();
                            MoveOneLeft();
                            MoveOneBackwardsDiagonallyLeft();
                            MoveOneBackwardsDiagonallyRight();
                            break;
                    }
                }
            }
            if (rankIndex > 0)
            {
                if (IsWhite)
                {
                    MoveOneBackwards();
                    switch (fileIndex)
                    {
                        case 0:
                            MoveOneRight();
                            MoveOneBackwardsDiagonallyLeft();
                            break;
                        case 7:
                            MoveOneLeft();
                            MoveOneBackwardsDiagonallyRight();
                            break;
                        default:
                            MoveOneRight();
                            MoveOneLeft();
                            MoveOneBackwardsDiagonallyLeft();
                            MoveOneBackwardsDiagonallyRight();
                            break;
                    }
                }
                else
                {
                    MoveOneForward();
                    switch (fileIndex)
                    {
                        case 0:
                            MoveOneLeft();
                            MoveOneForwardDiagonallyLeft();
                            break;
                        case 7:
                            MoveOneRight();
                            MoveOneForwardDiagonallyRight();
                            break;
                        default:
                            MoveOneRight();
                            MoveOneLeft();
                            MoveOneForwardDiagonallyLeft();
                            MoveOneForwardDiagonallyRight();
                            break;
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

        private void MoveKing(int x_white, int y_white, int fileIndex, int rankIndex, Board board, HashSet<string> positions)
        {
            int x = IsWhite ? x_white : -x_white;
            int y = IsWhite ? y_white : -y_white;
            Field newField = board[fileIndex + x][rankIndex + y];

            ControlledSquares.Add(newField.Name);

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
