using System.Collections.Generic;

namespace Chess.Model.Pieces
{
    class Knight : Piece
    {
        static public new string[] PieceNames => new string[] { "Nw", "Nb" };

        public Knight(bool iswhite, string position)
        {
            IsWhite = iswhite;
            Position = position;
            Name = iswhite ? Name = PieceNames[0] : Name = PieceNames[1];
        }

        protected override List<string> ReturnCorrectPieceMoves(int fileIndex, int rankIndex, Board board, List<string> positions)
        {
            KnightMove(fileIndex, rankIndex, board, positions);
            return positions;
        }

        List<string> KnightMove(int fileIndex, int rankIndex, Board board, List<string> positions)
        {
            if (rankIndex < Board.boardSize - 2)
            {
                if (IsWhite)
                {
                    if (fileIndex == 0)
                    {
                        MoveTwoForwardOneRight();
                        MoveTwoRightOneForward();
                    }
                    else if(fileIndex == 1)
                    {
                        MoveTwoForwardOneRight();
                        MoveTwoRightOneForward();
                        MoveTwoForwardOneLeft();
                    }
                    else if (fileIndex == 6)
                    {
                        MoveTwoForwardOneLeft();
                        MoveTwoLeftOneForward();
                        MoveTwoForwardOneRight();
                    }
                    else if(fileIndex == 7)
                    {
                        MoveTwoForwardOneLeft();
                        MoveTwoLeftOneForward();
                    }
                    else
                    {
                        MoveTwoForwardOneRight();
                        MoveTwoForwardOneLeft();
                        MoveTwoRightOneForward();
                        MoveTwoLeftOneForward();
                    }
                }
                else
                {
                    if (fileIndex == 0)
                    {
                        MoveTwoBackwardsOneLeft();
                        MoveTwoLeftOneBackwards();

                    }
                    else if (fileIndex == 1)
                    {
                        MoveTwoBackwardsOneLeft();
                        MoveTwoLeftOneBackwards();
                        MoveTwoBackwardsOneRight();
                    }
                    else if (fileIndex == 6)
                    {
                        MoveTwoBackwardsOneRight();
                        MoveTwoRightOneBackwards();
                        MoveTwoBackwardsOneLeft();
                    }
                    else if (fileIndex == 7)
                    {
                        MoveTwoBackwardsOneRight();
                        MoveTwoRightOneBackwards();
                    }
                    else
                    {
                        MoveTwoBackwardsOneRight();
                        MoveTwoBackwardsOneLeft();
                        MoveTwoRightOneBackwards();
                        MoveTwoLeftOneBackwards();
                    }
                }
            }
            if (rankIndex > 1)
            {
                if (IsWhite)
                {
                    if (fileIndex == 0)
                    {
                        MoveTwoBackwardsOneRight();
                        MoveTwoRightOneBackwards();
                    }
                    else if (fileIndex == 1)
                    {
                        MoveTwoBackwardsOneRight();
                        MoveTwoRightOneBackwards();
                        MoveTwoBackwardsOneLeft();
                    }
                    else if (fileIndex == 6)
                    {
                        MoveTwoBackwardsOneLeft();
                        MoveTwoLeftOneBackwards();
                        MoveTwoBackwardsOneRight();
                    }
                    else if (fileIndex == 7)
                    {
                        MoveTwoBackwardsOneLeft();
                        MoveTwoLeftOneBackwards();
                    }
                    else
                    {
                        MoveTwoBackwardsOneRight();
                        MoveTwoBackwardsOneLeft();
                        MoveTwoRightOneBackwards();
                        MoveTwoLeftOneBackwards();
                    }
                }
                else
                {
                    if (fileIndex == 0)
                    {
                        MoveTwoForwardOneLeft();
                        MoveTwoLeftOneForward();
                    }
                    else if (fileIndex == 1)
                    {
                        MoveTwoForwardOneLeft();
                        MoveTwoLeftOneForward();
                        MoveTwoForwardOneRight();
                    }
                    else if (fileIndex == 6)
                    {
                        MoveTwoForwardOneRight();
                        MoveTwoRightOneForward();
                        MoveTwoForwardOneLeft();
                    }
                    else if (fileIndex == 7)
                    {
                        MoveTwoForwardOneRight();
                        MoveTwoRightOneForward();
                    }
                    else
                    {
                        MoveTwoForwardOneRight();
                        MoveTwoForwardOneLeft();
                        MoveTwoRightOneForward();
                        MoveTwoLeftOneForward();
                    }
                }
            }
            return positions;
            void MoveTwoForwardOneRight() => MoveKnight(1, 2, fileIndex, rankIndex, board, positions);
            void MoveTwoForwardOneLeft() => MoveKnight(-1, 2, fileIndex, rankIndex, board, positions);
            void MoveTwoBackwardsOneRight() => MoveKnight(1, -2, fileIndex, rankIndex, board, positions);
            void MoveTwoBackwardsOneLeft() => MoveKnight(-1, -2, fileIndex, rankIndex, board, positions);
            void MoveTwoRightOneForward() => MoveKnight(2, 1, fileIndex, rankIndex, board, positions);
            void MoveTwoRightOneBackwards() => MoveKnight(2, -1, fileIndex, rankIndex, board, positions);
            void MoveTwoLeftOneForward() => MoveKnight(-2, 1, fileIndex, rankIndex, board, positions);
            void MoveTwoLeftOneBackwards() => MoveKnight(-2, -1, fileIndex, rankIndex, board, positions);
        }

        void MoveKnight(int x_white, int y_white, int fileIndex, int rankIndex, Board board, List<string> positions)
        {
            int x = IsWhite ? x_white : -x_white;
            int y = IsWhite ? y_white : -y_white;

            Field newField = board[fileIndex + x][rankIndex + y];

            if (newField.Content == null)
            {
                positions.Add(Board.Files[fileIndex + x] + Board.Ranks[rankIndex + y]);
            }
            else
            {
                bool z = IsWhite ? !(newField.Content.IsWhite) : newField.Content.IsWhite;
                if (z)
                {
                    if (newField.Content.GetType() != typeof(King))
                    {
                        positions.Add(Board.Files[fileIndex + x] + Board.Ranks[fileIndex + y]);
                    }
                    else
                    {
                        //set king in check TO DO
                    }
                }
            }
        }
    }
}
