using System.Collections.Generic;

namespace Chess.Model.Pieces
{
    class Bishop : Piece, IDiagonallyMovingPiece
    {
        private static new string[] PieceNames { get; } = new string[] { "Bw", "Bb" };

        public Bishop(bool isWhite, string position)
        {
            IsWhite = isWhite;
            Position = position;
            Name = isWhite ? Name = PieceNames[0] : Name = PieceNames[1];  
        }

        protected override HashSet<string> ReturnCorrectPieceMoves(int fileIndex, int rankIndex, Board board, HashSet<string> positions)
        {
            MoveRightForward(fileIndex, rankIndex, board, positions);
            MoveLeftBackwards(fileIndex, rankIndex, board, positions);
            MoveLeftForward(fileIndex, rankIndex, board, positions);
            MoveRightBackwards(fileIndex, rankIndex, board, positions);
            return positions;
        }

        public void MoveRightForward(int fileIndex, int rankIndex, Board board, HashSet<string> positions)
        {
            bool canMove = true;
            bool kingInTheWay = false;
            int rank = rankIndex;
            int file = fileIndex;
            if (IsWhite)
            {
                while (file < Board.BoardSize - 1 && rank < Board.BoardSize - 1 && canMove)
                {
                    MoveOne(1, 1, ref file, ref rank, ref canMove, ref kingInTheWay, board, positions);
                }
            }
            else
            {
                while (file > 0 && rank > 0 && canMove)
                {
                    MoveOne(1, 1, ref file, ref rank, ref canMove, ref kingInTheWay, board, positions);
                }
            }
        }

        public void MoveLeftBackwards(int fileIndex, int rankIndex, Board board, HashSet<string> positions)
        {
            bool canMove = true;
            bool kingInTheWay = false;
            int rank = rankIndex;
            int file = fileIndex;
            if (IsWhite)
            {
                while (file > 0 && rank > 0 && canMove)
                {
                    MoveOne(-1, -1, ref file, ref rank, ref canMove, ref kingInTheWay, board, positions);
                }
            }
            else
            {
                while (file < Board.BoardSize - 1 && rank < Board.BoardSize - 1 && canMove)
                {
                    MoveOne(-1, -1, ref file, ref rank, ref canMove, ref kingInTheWay, board, positions);
                }
            }
        }

        public void MoveLeftForward(int fileIndex, int rankIndex, Board board, HashSet<string> positions)
        {
            bool canMove = true;
            bool kingInTheWay = false;
            int rank = rankIndex;
            int file = fileIndex;
            if (IsWhite)
            {
                while (file > 0 && rank < Board.BoardSize - 1 && canMove)
                {
                    MoveOne(-1, 1, ref file, ref rank, ref canMove, ref kingInTheWay, board, positions);
                }
            }
            else
            {
                while (file < Board.BoardSize - 1 && rank > 0 && canMove)
                {
                    MoveOne(-1, 1, ref file, ref rank, ref canMove, ref kingInTheWay, board, positions);
                }
            }
        }

        public void MoveRightBackwards(int fileIndex, int rankIndex, Board board, HashSet<string> positions)
        {
            bool canMove = true;
            bool kingInTheWay = false;
            int rank = rankIndex;
            int file = fileIndex;
            if (IsWhite)
            {
                while (file < Board.BoardSize - 1 && rank > 0 && canMove)
                {
                    MoveOne(1, -1, ref file, ref rank, ref canMove, ref kingInTheWay, board, positions);
                }
            }
            else
            {
                while (file > 0 && rank < Board.BoardSize - 1 && canMove)
                {
                    MoveOne(1, -1, ref file, ref rank, ref canMove, ref kingInTheWay, board, positions);
                }
            }
        }

        public void MoveOne(int x_white, int y_white, ref int file, ref int rank, ref bool canMove, ref bool kingInTheWay, Board board, HashSet<string> positions)
        {
            int x = IsWhite ? x_white : -x_white;
            int y = IsWhite ? y_white : -y_white;
            Field newField = board[file + x][rank + y];

            ControlledSquares.Add(newField.Name);

            if (newField.Content == null)
            {
                if (!kingInTheWay)
                {
                    positions.Add(newField.Name);
                }
                file += x;
                rank += y;
            }
            else
            {
                bool z = IsWhite ? !(newField.Content.IsWhite) : newField.Content.IsWhite;
                if (z)
                {
                    if (newField.Content.GetType() != typeof(King))
                    {
                        if (!kingInTheWay)
                        {
                            positions.Add(newField.Name);
                        }
                        canMove = false;
                    }
                    else
                    {
                        if (IsWhite)
                        {
                            GameState.BlackKingIsInCheck = true;
                        }
                        else
                        {
                            GameState.WhiteKingIsInCheck = true;
                        }
                        GameState.CurrentPlayerPiecesAttackingTheKing.Add(this);

                        kingInTheWay = true;
                        file += x;
                        rank += y;
                    }
                }
                else
                {
                    canMove = false;
                }
            }
        }
    }
}
