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

            void MoveOneForwardDiagonallyLeft() // from the 'piece position of view' - black moves right on the board view
            {
                MoveOne(-1, 1);
            }

            void MoveOneBackwardsDiagonallyLeft() // from the 'piece position of view' - black moves right on the board view
            {
                MoveOne(-1, -1);
            }

            void MoveOneForwardDiagonallyRight() // from the 'piece position of view' - black moves left on the board view
            {
                MoveOne(1, 1);
            }

            void MoveOneBackwardsDiagonallyRight() // from the 'piece position of view' - black moves left on the board view
            {
                MoveOne(1, -1);
            }

            void MoveOneForward()       // TODO : król nie powinien móc się ruszyć na pole które jest atakowane przez figurę przeciwnika 
            {                           // ( jeśli NextAvailablePositions którejś z figur przeciwnika zawiera pole na które chce się ruszyć król)
                MoveOne(0, 1);
            }

            void MoveOneBackwards()
            {
                MoveOne(0, -1);
            }

            void MoveOneRight()
            {
                MoveOne(1, 0);
            }

            void MoveOneLeft()
            {
                MoveOne(-1, 0);
            }

            void CheckSquare(int x, int y) // wykorzystać funkcję do napisania bardziej ogólnej? (wektor) TODO       !!!!!
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
            /*
            void MovePiece()
            {
                //queen
                while (rankIndex < Board.boardSize - 1
                       && newField.Content == null)
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
            }*/
            /*
            void MoveVerticaly(int y)
            {
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

            void MoveHorizontaly(int x)
            {
                newField = board[fileIndex + x][rankIndex];
                if (newField.Content == null)
                {
                    positions.Add(Board.Files[fileIndex + x] + Board.Ranks[rankIndex]);
                }
                if (newField.Content != null && newField.Content.GetType() != typeof(King))
                {
                    bool z = IsWhite ? !(newField.Content.IsWhite) : newField.Content.IsWhite;
                    if (z)
                    {
                        positions.Add(Board.Files[fileIndex + x] + Board.Ranks[rankIndex]);
                    }
                }
            }*/

        }
        /*
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
            if (rankIndex == 7)
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
        }*/
    }
}