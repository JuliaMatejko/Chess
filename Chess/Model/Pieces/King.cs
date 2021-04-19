using System;
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

        protected override List<string> ReturnCorrectPieceMoves(int fileIndex, int rankIndex, Field newField, Board board, List<string> positions)
        {
            //type of moves: vertical, horizontal, diagonal, not in line of attack,special: castle
            KingMove(positions, fileIndex, rankIndex, newField, board);
            //Move validation! not in line of attack();
            //CastleKingRookMove(); czy powinien byc w tej klasie?

            return positions;

        }

        List<string> KingMove(List<string> positions, int fileIndex, int rankIndex, Field newField, Board board)
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

            void MoveOne(int x_white, int y_white)//, int x_black, int y_black)
            {
                int x = IsWhite ? x_white : -x_white;
                int y = IsWhite ? y_white : -y_white;
                CheckSquare(x, y);
            }
            void MoveOneForwardDiagonallyLeft() => MoveKing(-1, 1, fileIndex, rankIndex, newField, board, positions);
            void MoveOneBackwardsDiagonallyLeft() => MoveKing(-1, -1, fileIndex, rankIndex, newField, board, positions);
            void MoveOneForwardDiagonallyRight() => MoveKing(1, 1, fileIndex, rankIndex, newField, board, positions);
            void MoveOneBackwardsDiagonallyRight() => MoveKing(1, -1, fileIndex, rankIndex, newField, board, positions);
            void MoveOneForward() => MoveKing(0, 1, fileIndex, rankIndex, newField, board, positions);
            void MoveOneBackwards() => MoveKing(0, -1, fileIndex, rankIndex, newField, board, positions);
            void MoveOneRight() => MoveKing(1, 0, fileIndex, rankIndex, newField, board, positions);
            void MoveOneLeft() => MoveKing(-1, 0, fileIndex, rankIndex, newField, board, positions);

            void CheckSquare(int x, int y)
            {
                newField = board[fileIndex + x][rankIndex + y];
                if (newField.Content == null)
                {
                    positions.Add(Board.Files[fileIndex + x] + Board.Ranks[rankIndex + y]);
                }
                if (newField.Content != null && newField.Content.GetType() != typeof(King))
                {
                    bool z = IsWhite ? !(newField.Content.IsWhite) : newField.Content.IsWhite;
                    if (z)
                    {
                        positions.Add(Board.Files[fileIndex + x] + Board.Ranks[rankIndex + y]);
                    }
                }
            }
        }

        void MoveKing(int x_white, int y_white, int fileIndex, int rankIndex, Field newField, Board board, List<string> positions)
        {
            int x = IsWhite ? x_white : -x_white;
            int y = IsWhite ? y_white : -y_white;
            
            newField = board[fileIndex + x][rankIndex + y];

            if (newField.Content == null)
            {
                positions.Add(Board.Files[fileIndex + x] + Board.Ranks[rankIndex + y]);
            }
            else
            {
                bool z = IsWhite ? !(newField.Content.IsWhite) : newField.Content.IsWhite;
                if (z && newField.Content.GetType() != typeof(King))
                {
                    positions.Add(Board.Files[fileIndex + x] + Board.Ranks[rankIndex + y]);
                }
            }
             
        }
    }
}