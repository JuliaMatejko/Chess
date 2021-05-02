using System;
using System.Collections.Generic;

namespace Chess.Model.Pieces
{
    abstract class Piece
    {
        public bool IsWhite { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public List<string> NextAvailablePositions => ReturnAvailablePieceMoves(Position, Game.board);
        public static string[] PieceNames => new string[] { "pw", "pb", "Rw", "Rb", "Nw", "Nb", "Bw", "Bb", "Qw", "Qb", "Kw", "Kb" };

        protected List<string> ReturnAvailablePieceMoves(string currentposition, Board board)
        {
            int fileIndex = Array.IndexOf(Board.Files, Convert.ToString(currentposition[0]));
            int rankIndex = Array.IndexOf(Board.Ranks, Convert.ToString(currentposition[1]));
            List<string> positions = new List<string>();
            positions.AddRange(ReturnCorrectPieceMoves(fileIndex, rankIndex, board, positions));
            return positions;
        }

        protected abstract List<string> ReturnCorrectPieceMoves(int fileIndex, int rankIndex, Board board, List<string> positions);

        protected void MoveForward(int fileIndex, int rankIndex, Board board, List<string> positions)
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

        protected void MoveBackwards(int fileIndex, int rankIndex, Board board, List<string> positions)
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

        protected void MoveLeft(int fileIndex, int rankIndex, Board board, List<string> positions)
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

        protected void MoveRight(int fileIndex, int rankIndex, Board board, List<string> positions)
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

        protected void MoveRightForward(int fileIndex, int rankIndex, Board board, List<string> positions)
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

        protected void MoveLeftBackwards(int fileIndex, int rankIndex, Board board, List<string> positions)
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

        protected void MoveLeftForward(int fileIndex, int rankIndex, Board board, List<string> positions)
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

        internal abstract Piece Clone();//to do, if null return null

        protected void MoveRightBackwards(int fileIndex, int rankIndex, Board board, List<string> positions)
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

        protected void MoveOne(int x_white, int y_white, ref int file, ref int rank, ref bool canMove, Board board, List<string> positions)
        {
            int x = IsWhite ? x_white : -x_white;
            int y = IsWhite ? y_white : -y_white;
            Field newField = board[file + x][rank + y];
            if (newField.Content == null)
            {
                positions.Add(Board.Files[file + x] + Board.Ranks[rank + y]);
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
                        positions.Add(Board.Files[file + x] + Board.Ranks[rank + y]);
                    }
                    else
                    {
                        //set king in check TO DO
                    }
                }
                canMove = false;
            }
        }
    }
}

