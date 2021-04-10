using System;
using System.Collections.Generic;

namespace Chess.Model.Pieces
{
    class Pawn : Piece
    {
        static public new string[] PieceNames => new string[] { "pw", "pb" };
        public bool IsFirstMove { get; set; } = true;
        override public List<string> NextAvailablePositions => ReturnAvailablePieceMoves(Position, Game.board);
        public Pawn(bool iswhite, string position)
        {
            IsWhite = iswhite;
            Position = position;
            if (iswhite == true)
            {
                Name = PieceNames[0];
            }
            else
            {
                Name = PieceNames[1];
            }
            NextAvailablePositions = ReturnAvailablePieceMoves(Position, Game.board);
        }

        List<string> ReturnAvailablePieceMoves(string currentposition, Board board)
        {
            List<string> positions = new List<string>();
            positions.AddRange(StandardPawnMove(currentposition, board));
            //positions.AddRange(PawnCapturesPieceMove(currentposition, board));
            foreach (var item in positions)
            {
                Console.WriteLine(item);
            }
            return positions;
           
        }

        /*
        void CorrectPawnMove(string currentposition, Board board)
        {
            /*
             * Pawns have the most complex rules of movement:
              1.  A pawn moves straight forward one square, if that square is vacant.    a2 -> a3
              2. If it has not yet moved, a pawn also has the option of moving two squares straight forward, provided both squares are vacant.   a2 -> a4
              3. Pawns cannot move backwards. a2 -/-> a1

             4. A pawn, unlike other pieces, captures differently from how it moves. A pawn can capture an enemy piece on either of the two squares diagonally in front of the pawn 
                (but cannot move to those squares if they are vacant).
             5. The pawn is also involved in the two special moves en passant and promotion
                    - promotion:
                        If a player advances a pawn to its eighth rank, the pawn is then promoted (converted) to a queen, rook, bishop, or knight of the same color
                        at the choice of the player (a queen is usually chosen). The choice is not limited to previously captured pieces.
                    - en passant:
                        When a pawn advances two squares from its original square and ends the turn adjacent to a pawn of the opponent's on the same rank, it may be captured by
                        that pawn of the opponent's, as if it had moved only one square forward. This capture is only legal on the opponent's next move immediately following 
                        the first pawn's advance.


            Ad 1. Ad 2. obecna pozycja +1 if its first move, obecna pozycja +1 lub +2 (granicą jest boardSize)

             
            StandardPawnMove(currentposition, board);
        }
    */

        List<string> StandardPawnMove(string currentposition, Board board) //zwraca listę ruchów
        {
            List<string> positions = new List<string>();

            string f = Convert.ToString(currentposition[0]);
            string r = Convert.ToString(currentposition[1]);
            int file = Array.IndexOf(Board.Files, f);
            int rank = Array.IndexOf(Board.Ranks, r);
            //string newposition;

            if (IsFirstMove == true && rank < Board.boardSize - 2)
            {
                string newposition = Board.Files[file] + Board.Ranks[rank + 2];
                if (board[file][rank + 2].Content == null)
                {
                    positions.Add(newposition);
                }
            }
            if (rank < Board.boardSize)
            {
                string newposition = Board.Files[file] + Board.Ranks[rank + 1];
                if (board[file][rank + 1].Content == null)
                {
                    positions.Add(newposition);
                }
            }
            return positions;
        }
        /*
        List<string> PawnCapturesPieceMove(string currentposition, Board board) // zwraca listę ruchów. Co z nią zroboić?
        {
            List<string> positions = new List<string>();

            string f = Convert.ToString(currentposition[0]);
            string r = Convert.ToString(currentposition[1]);
            int file = Array.IndexOf(Board.Files, f);
            int rank = Array.IndexOf(Board.Ranks, r);
            string newposition;

            if (rank < Board.boardSize)
            {
                if (file == 0)
                {
                    newposition = Board.Files[file + 1] + Board.Ranks[rank + 1];
                    if (board[file + 1][rank + 1].Content != null)
                    {
                        if ((board[file + 1][rank + 1].Content.IsWhite) && (GameState.CurrentPlayer == GameState.Sides.White))
                        {
                            positions.Add(newposition);
                        }
                        else if (!(board[file + 1][rank + 1].Content.IsWhite) && (GameState.CurrentPlayer == GameState.Sides.Black))
                        {
                            positions.Add(newposition);
                        }
                    }
                }
                else if (file == 7)
                {
                    newposition = Board.Files[file - 1] + Board.Ranks[rank + 1];
                    if (board[file - 1][rank + 1].Content != null)
                    {
                        if ((board[file - 1][rank + 1].Content.IsWhite) && (GameState.CurrentPlayer == GameState.Sides.White))
                        {
                            positions.Add(newposition);
                        }
                        else if (!(board[file - 1][rank + 1].Content.IsWhite) && (GameState.CurrentPlayer == GameState.Sides.Black))
                        {
                            positions.Add(newposition);
                        }
                    }
                }
                else
                {
                    newposition = Board.Files[file + 1] + Board.Ranks[rank + 1];
                    if (board[file + 1][rank + 1].Content != null)
                    {
                        if ((board[file + 1][rank + 1].Content.IsWhite) && (GameState.CurrentPlayer == GameState.Sides.White))
                        {
                            positions.Add(newposition);
                        }
                        else if (!(board[file + 1][rank + 1].Content.IsWhite) && (GameState.CurrentPlayer == GameState.Sides.Black))
                        {
                            positions.Add(newposition);
                        }
                    }
                    newposition = Board.Files[file - 1] + Board.Ranks[rank + 1];
                    if (board[file - 1][rank + 1].Content != null)
                    {
                        if ((board[file - 1][rank + 1].Content.IsWhite) && (GameState.CurrentPlayer == GameState.Sides.White))
                        {
                            positions.Add(newposition);
                        }
                        else if (!(board[file - 1][rank + 1].Content.IsWhite) && (GameState.CurrentPlayer == GameState.Sides.Black))
                        {
                            positions.Add(newposition);
                        }
                    }
                }
            }
            return positions;
        }*/
        //TODO :
        // test and implement ^ v !
        //EnPassantCaptureMove()
        //PromotionMove()
    }
}