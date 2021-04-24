using System;
using System.Collections.Generic;

namespace Chess.Model.Pieces
{
    class Pawn : Piece
    {
        public static new string[] PieceNames => new string[] { "pw", "pb" };
        public bool IsFirstMove { get; set; } = true;
        public bool CanBeTakenByEnPassantMove { get; set; } = false;
        public List<string> ControlledSquares => ReturnControlledSquares(Position);

        public Pawn(bool iswhite, string position)
        {
            IsWhite = iswhite;
            Position = position;
            Name = iswhite ? Name = PieceNames[0] : Name = PieceNames[1];
        }

        protected override List<string> ReturnCorrectPieceMoves(int fileIndex, int rankIndex, Board board, List<string> positions)
        {
            StandardPawnMove(fileIndex, rankIndex, board, positions);
            EnPassantPawnMove(fileIndex, rankIndex, positions);
            return positions;
        }

        List<string> StandardPawnMove(int fileIndex, int rankIndex, Board board, List<string> positions)
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
        
        List<string> EnPassantPawnMove(int fileIndex, int rankIndex, List<string> positions) // refactor
        {
            /*
                Jeśli przeciwnik:
            - wykonał w poprzednim (!) ruchu ruch pionem ///  Iswhite  board[x][4].Content.CanBeTakenByEnPassantMove == true / board[x][3].Content.CanBeTakenByEnPassantMove == true
            - wykonał ruch o 2 pola v
            V
            
            i jeśli:
            - któryś nasz pion znajduje się w tym samym rzędzie //// IsWhite    rankIndex == 4  /   rankIndex == 3
            - w poprzedniej lub kolejnej kolumnie w stosunku do tego piona przeciwnika  IsWhite     fileIndex == x+1 || fileIndex == x-1

            To:

            -Pion może zbić pion przeciwnika, poruszając się na pole zaraz za nim ( ta sama kolumna, wyższy rząd)
            IsWhite     positions.Add(Board.Files[fileIndex + x] + Board.Ranks[5]);     /   positions.Add(Board.Files[fileIndex + x] + Board.Ranks[4]);

             */


            if (IsWhite)
            {
                if (rankIndex == 4 && GameState.BlackPawnThatCanBeTakenByEnPassantMove != null)
                {
                    string oponentPawnFile = Convert.ToString(GameState.BlackPawnThatCanBeTakenByEnPassantMove.Position[0]);
                    if (fileIndex == 0)
                    {
                        if (oponentPawnFile == Board.Files[fileIndex + 1])
                        {
                            EnPassantRight();
                        } 
                    }
                    else if (fileIndex == 7)
                    {
                        if (oponentPawnFile == Board.Files[fileIndex - 1])
                        {
                            EnPassantLeft();
                        }
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
                    if (fileIndex == 0)
                    {
                        if (oponentPawnFile == Board.Files[fileIndex + 1])
                        {
                            EnPassantLeft();
                        }
                    }
                    else if (fileIndex == 7)
                    {
                        if (oponentPawnFile == Board.Files[fileIndex - 1])
                        {
                            EnPassantRight();
                        }
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

            void EnPassant(int x_white, int y_white, int fileIndex, int rankIndex, List<string> positions)
            {
                int x = IsWhite ? x_white : -x_white;
                int y = IsWhite ? y_white : -y_white;
                positions.Add(Board.Files[fileIndex + x] + Board.Ranks[rankIndex + y]);
            }
            return positions;
        }

        void MovePawn(int x_white, int y_white, int fileIndex, int rankIndex, Board board, List<string> positions)
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
            if (newField.Content != null && (x_white == -1 || x_white == 1) && y_white == 1)
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

        public void PawnPromotion(Move move)
        {
            /*
                - Bishop, Knight, Rook or Queen? 'piece currentpos newpos B|N|R|Q' for ex. 'pw a7 a8 Q'
             */
            switch (move.PromotionTo)
            {
                case "Q":
                    Game.Fields[move.NewPosition].Content = new Queen(IsWhite, move.NewPosition);
                    break;
                case "N":
                    Game.Fields[move.NewPosition].Content = new Knight(IsWhite, move.NewPosition);
                    break;
                case "R":
                    Game.Fields[move.NewPosition].Content = new Rook(IsWhite, move.NewPosition);
                    break;
                case "B":
                    Game.Fields[move.NewPosition].Content = new Bishop(IsWhite, move.NewPosition);
                    break;
            }
            Game.Fields[move.CurrentPosition].Content = null;
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

