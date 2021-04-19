using System;
using System.Collections.Generic;

namespace Chess.Model.Pieces
{
    class Pawn : Piece
    {
        public static new string[] PieceNames => new string[] { "pw", "pb" };
        public bool IsFirstMove { get; set; } = true;
        public List<string> ControlledSquares => ReturnControlledSquares(Position);

        public Pawn(bool iswhite, string position)
        {
            IsWhite = iswhite;
            Position = position;
            Name = iswhite ? Name = PieceNames[0] : Name = PieceNames[1];
        }

        protected override List<string> ReturnCorrectPieceMoves(int fileIndex, int rankIndex, Board board, List<string> positions)
        {
            StandardPawnMove(positions, fileIndex, rankIndex, board);
            //EnPassantPawnMove(); TODO
            //PromotionPawnMove(); TODO
            return positions;
        }

        List<string> StandardPawnMove(List<string> positions, int fileIndex, int rankIndex, Board board)
        {   
            if (IsWhite)
            {
                if (IsFirstMove)
                {
                    MoveTwoForward();
                }
                if (rankIndex < Board.boardSize - 1)
                {
                    MoveOneForward();
                    if (fileIndex == 0)
                    {
                        MoveOneForwardDiagonallyRight();
                    }
                    else if (fileIndex == 7)
                    {
                        MoveOneForwardDiagonallyLeft();
                    }
                    else
                    {
                        MoveOneForwardDiagonallyRight();
                        MoveOneForwardDiagonallyLeft();
                    }
                }
                return positions;
            }
            else
            {
                if (IsFirstMove)
                {
                    MoveTwoForward();
                }
                if (rankIndex > 0)
                {
                    MoveOneForward();
                    if (fileIndex == 0)
                    {
                        MoveOneForwardDiagonallyLeft();
                    }
                    else if (fileIndex == 7)
                    {
                        MoveOneForwardDiagonallyRight();
                    }
                    else
                    {
                        MoveOneForwardDiagonallyLeft();
                        MoveOneForwardDiagonallyRight();
                    }
                }
                return positions;
            }
            void MoveOneForward() => MovePawn(0, 1, fileIndex, rankIndex, board, positions);
            void MoveTwoForward() => MovePawn(0, 2, fileIndex, rankIndex, board, positions);
            void MoveOneForwardDiagonallyRight() => MovePawn(1, 1, fileIndex, rankIndex, board, positions);
            void MoveOneForwardDiagonallyLeft() => MovePawn(-1, 1, fileIndex, rankIndex, board, positions);
        }

        void MovePawn(int x_white, int y_white, int fileIndex, int rankIndex, Board board, List<string> positions)
        {
            int x = IsWhite ? x_white : -x_white;
            int y = IsWhite ? y_white : -y_white;
            Field newField = board[fileIndex + x][rankIndex + y];
            if (newField.Content == null)
            {
                positions.Add(Board.Files[fileIndex] + Board.Ranks[rankIndex + y]);
            }
            else
            {
                bool z = IsWhite ? !(newField.Content.IsWhite) : newField.Content.IsWhite;
                if (z)
                {
                    if (newField.Content.GetType() != typeof(King))
                    {
                        positions.Add(Board.Files[fileIndex + x] + Board.Ranks[rankIndex + y]);
                    }
                    else
                    {
                        //set king in check TO DO
                    }
                }
            }
        }
        
        List<string> ReturnControlledSquares(string currentposition)
        {
            int fileIndex = Array.IndexOf(Board.Files, Convert.ToString(currentposition[0]));
            int rankIndex = Array.IndexOf(Board.Ranks, Convert.ToString(currentposition[1]));
            List<string> positions = new List<string>();
            int x = IsWhite ? 1 : -1;
            int y = IsWhite ? 1 : -1;
            if (IsWhite)
            {
                if (rankIndex < Board.boardSize - 1)
                {
                    if (fileIndex == 0)
                    {
                        positions.Add(Board.Files[fileIndex + x] + Board.Ranks[rankIndex + y]);
                    }
                    else if (fileIndex == 7)
                    {
                        positions.Add(Board.Files[fileIndex - x] + Board.Ranks[rankIndex + y]);
                    }
                    else
                    {
                        positions.Add(Board.Files[fileIndex + x] + Board.Ranks[rankIndex + y]);
                        positions.Add(Board.Files[fileIndex - x] + Board.Ranks[rankIndex + y]);
                    }
                }
                return positions;
            }
            else
            {
                if (rankIndex > 0)
                {
                    if (fileIndex == 0)
                    {
                        positions.Add(Board.Files[fileIndex - x] + Board.Ranks[rankIndex + y]);
                    }
                    else if (fileIndex == 7)
                    {
                        positions.Add(Board.Files[fileIndex + x] + Board.Ranks[rankIndex + y]);
                    }
                    else
                    {
                        positions.Add(Board.Files[fileIndex - x] + Board.Ranks[rankIndex + y]);
                        positions.Add(Board.Files[fileIndex + x] + Board.Ranks[rankIndex + y]);
                    }
                }  
            }
            return positions;
        }
    }
}
