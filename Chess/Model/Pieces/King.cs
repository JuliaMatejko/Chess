using System;
using System.Collections.Generic;

namespace Chess.Model.Pieces
{
    class King : Piece
    {
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
            VerticalKingMove(positions, fileIndex, rankIndex, newField, board);
            //HorizontalKingMove(positions, fileIndex, rankIndex, newField, board);
            DiagonalKingMove(positions, fileIndex, rankIndex, newField, board);
            //Move validation! not in line of attack();
            //CastleKingRookMove(); czy powinien byc w tej klasie?

            return positions;

        }

        List<string> DiagonalKingMove(List<string> positions, int fileIndex, int rankIndex, Field newField, Board board)
        {
            if (rankIndex < Board.boardSize - 1)
            {
                if (IsWhite)
                {
                    if (fileIndex == 0)
                    {
                        MoveOneForwardDiagonallyRight();
                    }
                    if (fileIndex == 7)
                    {
                        MoveOneForwardDiagonallyLeft();
                    }
                    else
                    {
                        MoveOneForwardDiagonallyRight();
                        MoveOneForwardDiagonallyLeft();
                    }
                }
                else
                {
                    if (fileIndex == 0)
                    {
                        MoveOneBackwardsDiagonallyLeft();
                    }
                    if (fileIndex == 7)
                    {
                        MoveOneBackwardsDiagonallyRight();
                    }
                    else
                    {
                        MoveOneBackwardsDiagonallyLeft();
                        MoveOneBackwardsDiagonallyRight();
                    }
                }
                if (rankIndex > 0)
                {
                    if (IsWhite)
                    {
                        if (fileIndex == 0)
                        {
                            MoveOneBackwardsDiagonallyRight();
                        }
                        if (fileIndex == 7)
                        {
                            MoveOneBackwardsDiagonallyLeft();
                        }
                        else
                        {
                            MoveOneBackwardsDiagonallyLeft();
                            MoveOneBackwardsDiagonallyRight();
                        }
                    }
                    else
                    {
                        if (fileIndex == 0)
                        {
                            MoveOneForwardDiagonallyLeft();
                        }
                        if (fileIndex == 7)
                        {
                            MoveOneForwardDiagonallyRight();
                        }
                        else
                        {
                            MoveOneForwardDiagonallyRight();
                            MoveOneForwardDiagonallyLeft();
                        }
                    }
                }
            }
            return positions;


            void MoveOneForwardDiagonallyLeft() // from the 'piece position of view' - black moves right on the board view
            {
                int x = IsWhite ? -1 : 1;
                int y = IsWhite ? 1 : -1;
                MoveDiagonally(x, y);
            }

            void MoveOneBackwardsDiagonallyLeft() // from the 'piece position of view' - black moves right on the board view
            {
                int x = IsWhite ? -1 : 1;
                int y = IsWhite ? -1 : 1;
                MoveDiagonally(x, y);
            }

            void MoveOneForwardDiagonallyRight() // from the 'piece position of view' - black moves left on the board view
            {
                int x = IsWhite ? 1 : -1;
                int y = IsWhite ? 1 : -1;
                MoveDiagonally(x, y);
            }

            void MoveOneBackwardsDiagonallyRight() // from the 'piece position of view' - black moves left on the board view
            {
                int x = IsWhite ? 1 : -1;
                int y = IsWhite ? -1 : 1;
                MoveDiagonally(x, y);
            }

            void MoveDiagonally(int x, int y) // wykorzystać funkcję do napisania bardziej ogólnej? (wektor) TODO
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

        List<string> HorizontalKingMove(List<string> positions, int fileIndex, int rankIndex, Field newField, Board board)
        {
            throw new NotImplementedException();
        }

        List<string> VerticalKingMove(List<string> positions, int fileIndex, int rankIndex, Field newField, Board board)
        {
            if (rankIndex < Board.boardSize - 1)
            {
                if (IsWhite)
                {
                    MoveOneForward();
                }
                else
                {
                    MoveOneBackwards();
                }
            }
            if (rankIndex > 0)
            {
                if (IsWhite)
                {
                    MoveOneBackwards();
                }
                else
                {
                    MoveOneForward();
                }
            }
            return positions;
            
            void MoveOneForward()       // TODO : król nie powinien móc się ruszyć na pole które jest atakowane przez figurę przeciwnika 
            {                           // ( jeśli NextAvailablePositions którejś z figur przeciwnika zawiera pole na które chce się ruszyć król)
                int y = IsWhite ? 1 : -1;
                newField = board[fileIndex][rankIndex + y];
                if (newField.Content == null)
                {
                    positions.Add(Board.Files[fileIndex] + Board.Ranks[rankIndex + y]);
                }
                if (newField.Content != null && newField.Content.GetType() != typeof(King))
                {
                    bool z = IsWhite ? !(newField.Content.IsWhite) : newField.Content.IsWhite;
                    if (z)
                    {
                        positions.Add(Board.Files[fileIndex] + Board.Ranks[rankIndex + y]);
                    }
                }
            }

            void MoveOneBackwards()
            {
                int y = IsWhite ? -1 : 1;
                newField = board[fileIndex][rankIndex + y];
                if (newField.Content == null)
                {
                    positions.Add(Board.Files[fileIndex] + Board.Ranks[rankIndex + y]);
                }
                if (newField.Content != null && newField.Content.GetType() != typeof(King))
                {
                    bool z = IsWhite ? !(newField.Content.IsWhite) : newField.Content.IsWhite;
                    if (z)
                    {
                        positions.Add(Board.Files[fileIndex] + Board.Ranks[rankIndex + y]);
                    }
                }
            }
        }
    }
}