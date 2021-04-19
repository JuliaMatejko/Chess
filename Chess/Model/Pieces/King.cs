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
            void MoveOneForwardDiagonallyLeft() => MoveOne(-1, 1);
            void MoveOneBackwardsDiagonallyLeft() => MoveOne(-1, -1);
            void MoveOneForwardDiagonallyRight() => MoveOne(1, 1);
            void MoveOneBackwardsDiagonallyRight() => MoveOne(1, -1);
            void MoveOneForward() => MoveOne(0, 1);
            void MoveOneBackwards() => MoveOne(0, -1);
            void MoveOneRight() => MoveOne(1, 0);
            void MoveOneLeft() => MoveOne(-1, 0);

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
            bool[,] oponentControlledSquares = IsWhite ? GameState.BlackControlledSquares : GameState.WhiteControlledSquares;
            newField = board[fileIndex + x][rankIndex + y];
            // TODO : król nie powinien móc się ruszyć na pole które jest atakowane przez figurę przeciwnika 
            // ( jeśli NextAvailablePositions którejś z figur przeciwnika zawiera pole na które chce się ruszyć król)
            if (!oponentControlledSquares[fileIndex + x, rankIndex + y])
            {
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
}