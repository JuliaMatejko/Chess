using System;
using System.Collections.Generic;

namespace Chess.Model.Pieces
{
    class Pawn : Piece
    {
        public static new string[] PieceNames => new string[] { "pw", "pb" };
        public bool IsFirstMove { get; set; } = true;
        public bool CanBeTakenByEnPassantMove { get; set; } = false;
        public HashSet<string> ControlledSquares { get; set; } = new HashSet<string>();

        public Pawn(bool iswhite, string position)
        {
            IsWhite = iswhite;
            Position = position;
            Name = iswhite ? Name = PieceNames[0] : Name = PieceNames[1];
        }

        protected override HashSet<string> ReturnCorrectPieceMoves(int fileIndex, int rankIndex, Board board, HashSet<string> positions)
        {
            StandardPawnMove(fileIndex, rankIndex, board, positions);
            EnPassantPawnMove(fileIndex, rankIndex, positions);
            return positions;
        }

        HashSet<string> StandardPawnMove(int fileIndex, int rankIndex, Board board, HashSet<string> positions)
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
        
        HashSet<string> EnPassantPawnMove(int fileIndex, int rankIndex, HashSet<string> positions)
        {
            if (IsWhite)
            {
                if (rankIndex == 4 && GameState.BlackPawnThatCanBeTakenByEnPassantMove != null)
                {
                    string oponentPawnFile = Convert.ToString(GameState.BlackPawnThatCanBeTakenByEnPassantMove.Position[0]);
                    if (fileIndex == 0 && oponentPawnFile == Board.Files[fileIndex + 1])
                    { 
                        EnPassantRight();
                    }
                    else if (fileIndex == 7 && oponentPawnFile == Board.Files[fileIndex - 1])
                    {
                        EnPassantLeft();
                    }
                    else
                    {
                        if (oponentPawnFile == Board.Files[fileIndex + 1])
                        {
                            EnPassantRight();
                        }
                        if (oponentPawnFile == Board.Files[fileIndex - 1])
                        {
                            EnPassantLeft();
                        }
                    }
                }
            }
            else
            {
                if (rankIndex == 3 && GameState.WhitePawnThatCanBeTakenByEnPassantMove != null)
                {
                    string oponentPawnFile = Convert.ToString(GameState.WhitePawnThatCanBeTakenByEnPassantMove.Position[0]);
                    if (fileIndex == 0 && oponentPawnFile == Board.Files[fileIndex + 1])
                    {
                        EnPassantLeft();
                    }
                    else if (fileIndex == 7 && oponentPawnFile == Board.Files[fileIndex - 1])
                    {
                        EnPassantRight();
                    }
                    else
                    {
                        if (oponentPawnFile == Board.Files[fileIndex + 1])
                        {
                            EnPassantLeft();
                        }
                        if (oponentPawnFile == Board.Files[fileIndex - 1])
                        {
                            EnPassantRight();
                        }
                    }
                }
            }
            void EnPassantRight() => EnPassant(1, 1, fileIndex, rankIndex, positions);
            void EnPassantLeft() => EnPassant(-1, 1, fileIndex, rankIndex, positions);

            void EnPassant(int x_white, int y_white, int fileIndex, int rankIndex, HashSet<string> positions)
            {
                int x = IsWhite ? x_white : -x_white;
                int y = IsWhite ? y_white : -y_white;
                positions.Add(Board.Files[fileIndex + x] + Board.Ranks[rankIndex + y]);
            }
            return positions;
        }

        void MovePawn(int x_white, int y_white, int fileIndex, int rankIndex, Board board, HashSet<string> positions)
        {
            int x = IsWhite ? x_white : -x_white;
            int y = IsWhite ? y_white : -y_white;
            Field newField = board[fileIndex + x][rankIndex + y];
            if (x_white == 0)
            {
                if (y_white == 2)
                {
                    int z = IsWhite ? -1 : 1;
                    if ((board[fileIndex][rankIndex + y + z].Content == null) && (newField.Content == null))
                    {
                        positions.Add(Board.Files[fileIndex] + Board.Ranks[rankIndex + y]);
                    }
                }
                if (y_white == 1)
                {
                    if (newField.Content == null)
                    {
                        positions.Add(Board.Files[fileIndex] + Board.Ranks[rankIndex + y]);
                    }
                }
            }
            if ((x_white == -1 || x_white == 1) && y_white == 1)
            {
                ControlledSquares.Add(Board.Files[fileIndex + x] + Board.Ranks[rankIndex + y]);
                if (newField.Content != null)
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
                }
            }
        }
    }
}

