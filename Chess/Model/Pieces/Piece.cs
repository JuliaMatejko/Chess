using System;
using System.Collections.Generic;

namespace Chess.Model.Pieces
{
    abstract class Piece : ICloneable
    {
        public bool IsWhite { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public HashSet<string> NextAvailablePositions { get; set; } = new HashSet<string>();
        public static string[] PieceNames => new string[] { "pw", "pb", "Rw", "Rb", "Nw", "Nb", "Bw", "Bb", "Qw", "Qb", "Kw", "Kb" };

        public HashSet<string> ReturnAvailablePieceMoves(string currentposition, Board board)
        {
            int fileIndex = Array.IndexOf(Board.Files, Convert.ToString(currentposition[0]));
            int rankIndex = Array.IndexOf(Board.Ranks, Convert.ToString(currentposition[1]));
            HashSet<string> positions = new HashSet<string>();
            positions = ReturnCorrectPieceMoves(fileIndex, rankIndex, board, positions);
            return positions;
        }

        protected abstract HashSet<string> ReturnCorrectPieceMoves(int fileIndex, int rankIndex, Board board, HashSet<string> positions);

        public object Clone()
        {
            return this != null ? MemberwiseClone() : null;
        }

        protected void MoveForward(int fileIndex, int rankIndex, Board board, HashSet<string> positions)
        {
            bool canMove = true;
            int rank = rankIndex;
            int file = fileIndex;
            if (IsWhite)
            {
                while (rank < Board.boardSize - 1 && canMove)
                {
                    MoveOne(0, 1, ref file, ref rank, ref canMove, board, positions);
                }
            }
            else
            {
                while (rank > 0 && canMove)
                {
                    MoveOne(0, 1, ref file, ref rank, ref canMove, board, positions);
                }
            }
        }

        protected void MoveBackwards(int fileIndex, int rankIndex, Board board, HashSet<string> positions)
        {
            bool canMove = true;
            int rank = rankIndex;
            int file = fileIndex;
            if (IsWhite)
            {
                while (rank > 0 && canMove)
                {
                    MoveOne(0, -1, ref file, ref rank, ref canMove, board, positions);
                }
            }
            else
            {
                while (rank < Board.boardSize - 1 && canMove)
                {
                    MoveOne(0, -1, ref file, ref rank, ref canMove, board, positions);
                }
            }
        }

        protected void MoveLeft(int fileIndex, int rankIndex, Board board, HashSet<string> positions)
        {
            bool canMove = true;
            int rank = rankIndex;
            int file = fileIndex;
            if (IsWhite)
            {
                while (file > 0 && canMove)
                {
                    MoveOne(-1, 0, ref file, ref rank, ref canMove, board, positions);
                }
            }
            else
            {
                while (file < Board.boardSize - 1 && canMove)
                {
                    MoveOne(-1, 0, ref file, ref rank, ref canMove, board, positions);
                }
            }
        }

        protected void MoveRight(int fileIndex, int rankIndex, Board board, HashSet<string> positions)
        {
            bool canMove = true;
            int rank = rankIndex;
            int file = fileIndex;
            if (IsWhite)
            {
                while (file < Board.boardSize - 1 && canMove)
                {
                    MoveOne(1, 0, ref file, ref rank, ref canMove, board, positions);
                }
            }
            else
            {
                while (file > 0 && canMove)
                {
                    MoveOne(1, 0, ref file, ref rank, ref canMove, board, positions);
                }
            }
        }

        protected void MoveRightForward(int fileIndex, int rankIndex, Board board, HashSet<string> positions)
        {
            bool canMove = true;
            int rank = rankIndex;
            int file = fileIndex;
            if (IsWhite)
            {
                while (file < Board.boardSize - 1 && rank < Board.boardSize - 1 && canMove)
                {
                    MoveOne(1, 1, ref file, ref rank, ref canMove, board, positions);
                }
            }
            else
            {
                while (file > 0 && rank > 0 && canMove)
                {
                    MoveOne(1, 1, ref file, ref rank, ref canMove, board, positions);
                }
            }
        }

        protected void MoveLeftBackwards(int fileIndex, int rankIndex, Board board, HashSet<string> positions)
        {
            bool canMove = true;
            int rank = rankIndex;
            int file = fileIndex;
            if (IsWhite)
            {
                while (file > 0 && rank > 0 && canMove)
                {
                    MoveOne(-1, -1, ref file, ref rank, ref canMove, board, positions);
                }
            }
            else
            {
                while (file < Board.boardSize - 1 && rank < Board.boardSize - 1 && canMove)
                {
                    MoveOne(-1, -1, ref file, ref rank, ref canMove, board, positions);
                }
            }
        }

        protected void MoveLeftForward(int fileIndex, int rankIndex, Board board, HashSet<string> positions)
        {
            bool canMove = true;
            int rank = rankIndex;
            int file = fileIndex;
            if (IsWhite)
            {
                while (file > 0 && rank < Board.boardSize - 1 && canMove)
                {
                    MoveOne(-1, 1, ref file, ref rank, ref canMove, board, positions);
                }
            }
            else
            {
                while (file < Board.boardSize - 1 && rank > 0 && canMove)
                {
                    MoveOne(-1, 1, ref file, ref rank, ref canMove, board, positions);
                }
            }
        }

        protected void MoveRightBackwards(int fileIndex, int rankIndex, Board board, HashSet<string> positions)
        {
            bool canMove = true;
            int rank = rankIndex;
            int file = fileIndex;
            if (IsWhite)
            {
                while (file < Board.boardSize - 1 && rank > 0 && canMove)
                {
                    MoveOne(1, -1, ref file, ref rank, ref canMove, board, positions);
                }
            }
            else
            {
                while (file > 0 && rank < Board.boardSize - 1 && canMove)
                {
                    MoveOne(1, -1, ref file, ref rank, ref canMove, board, positions);
                }
            }
        }

        protected void MoveOne(int x_white, int y_white, ref int file, ref int rank, ref bool canMove, Board board, HashSet<string> positions)
        {
            int x = IsWhite ? x_white : -x_white;
            int y = IsWhite ? y_white : -y_white;
            Field newField = board[file + x][rank + y];
            if (newField.Content == null)
            {
                positions.Add(newField.Name);
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
                        positions.Add(newField.Name);
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
                    }
                }
                canMove = false;
            }
        }
    }
}

